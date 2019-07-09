using System;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V4.Hardware.Fingerprint;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Model;

namespace DeliveryPersonApp.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText emailEditText, passwordEditText;
        Button signInButton, registerButton;        // I'm not entirely consistent with 2 word capitalisation here
        FingerprintManagerCompat fingerprintManager;
        Android.Support.V4.OS.CancellationSignal cancellation;
        ISharedPreferences preferences;
        string deliveryPersonId;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            fingerprintManager = FingerprintManagerCompat.From(this);   // this is context (ie device) for finger print readability check
            cancellation = new Android.Support.V4.OS.CancellationSignal();
            preferences = PreferenceManager.GetDefaultSharedPreferences(this);


            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            signInButton = FindViewById<Button>(Resource.Id.signInButton);
            registerButton = FindViewById<Button>(Resource.Id.registerSegueButton);     // NB to-register segue button not actual register button

            signInButton.Click += SignInButton_Click;
            registerButton.Click += RegisterButton_Click;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            StartActivity(typeof(RegisterActivity));
        }

        private async void SignInButton_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            // Bespoke method to check whether device can read fingerprints, to see whether local device (non-server db querying) biometric authentication can be used
            bool canUseFingerprint = CanUseFingerprint();

            if (canUseFingerprint)
            {
                LogUserIn();
            }
            else
            {
                //var deliveryPersonId = await DeliveryPerson.Login(emailEditText.Text, passwordEditText.Text);
                deliveryPersonId = await DeliveryPerson.Login(emailEditText.Text, passwordEditText.Text);
            }



            if (!String.IsNullOrEmpty(deliveryPersonId))
            {
                //preferences = PreferenceManager.GetDefaultSharedPreferences(this);

                var preferencesEditor = preferences.Edit();              // so as to save preferences, for recording the login name
                preferencesEditor.PutString("deliveryPersonId", deliveryPersonId);
                preferencesEditor.Apply();                              // persist on the device the login name/id

                Intent intent = new Intent(this, typeof(TabsActivity));   // Intent, for passing values (not needed if just starting Activity)
                intent.PutExtra("deliveryPersonId", deliveryPersonId);          // key/value passed via Intent to next activity (GetStringExtra from there)
                //StartActivity(typeof(TabsActivity));
                StartActivity(intent);                                  // must use the PutExtra'd intent to pass on data
            }
            else
            {
                Toast.MakeText(this, "Sign-In Failure", ToastLength.Long).Show();
            }
        }

        private void LogUserIn()
        {
            // throw new NotImplementedException();
            var cancellation = new Android.Support.V4.OS.CancellationSignal();

            FingerprintManagerCompat.AuthenticationCallback authenticationCallback = new AuthenticationCallback(this, deliveryPersonId);  // use our own authentication callback class (qv)

            fingerprintManager.Authenticate(null, 0, cancellation, authenticationCallback, null);
            // Authenticate parameters: crypto, flags (not used at mo), cancellation signal, callback (in class - cf helpers), handler (5 of)
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        /// <summary>
        /// Check whether this device can read a fingerprint
        /// </summary>
        /// <returns></returns>
        private bool CanUseFingerprint()
        {

            deliveryPersonId = preferences.GetString("deliveryPersonId", string.Empty);

            if (!string.IsNullOrEmpty(deliveryPersonId))    // if a login id has been saved on this device previously (hence possibility of local biometric authentication is possible)
            {
                if (fingerprintManager.IsHardwareDetected)  // if device can read fingerprints
                {
                    if (fingerprintManager.HasEnrolledFingerprints)     // if user has recorded her/his print on device
                    {
                        var permissionResult = ContextCompat.CheckSelfPermission(this, Manifest.Permission.UseFingerprint);
                        // Check permission from current user to use fingerprints
                        if (permissionResult == global::Android.Content.PM.Permission.Granted)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;       // any outcome other than the user granting of permission to use fingerprints: return false
        }
	}


    class AuthenticationCallback : FingerprintManagerCompat.AuthenticationCallback
    {
        Activity activity;
        string deliveryPersonId;

        public AuthenticationCallback(Activity activity, string deliveryPersonId)
        {
            this.activity = activity;
            this.deliveryPersonId = deliveryPersonId;   // needed?
        }

        public override void OnAuthenticationSucceeded(FingerprintManagerCompat.AuthenticationResult result)
        {
            base.OnAuthenticationSucceeded(result);

            Intent intent = new Intent(activity, typeof(TabsActivity));   // Intent, for passing values (not needed if just starting Activity)
            intent.PutExtra("deliveryPersonId", deliveryPersonId);          // key/value passed via Intent to next activity (GetStringExtra from there)
                                                                            //StartActivity(typeof(TabsActivity));
            activity.StartActivity(intent);                             // start the activity itself from itself (carrying PutExtra'd intent data)
        }

        public override void OnAuthenticationFailed()
        {
            base.OnAuthenticationFailed();

            Toast.MakeText(activity, "Login Failure", ToastLength.Long).Show();
        }
    }

}

