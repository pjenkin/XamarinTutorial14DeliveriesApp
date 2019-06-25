using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Model;
using Microsoft.WindowsAzure.MobileServices;
using NUnit;            // NUnit added as a test for whether visible from Droid project


namespace DeliveriesApp.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText emailEditText, passwordEditText;
        Button signInButton, registerButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {


            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
            // Leave-be all this boilerplate stuff for now - I dont want to risk breaking anything

            emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            signInButton = FindViewById<Button>(Resource.Id.signinButton);
            registerButton = FindViewById<Button>(Resource.Id.registerButton);
            // NB v similar to Java code for Android

            signInButton.Click += SignInButton_Click;          // NB autocomplete after '+='
            registerButton.Click += RegisterButton_Click;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            var intent = new Intent(this, typeof(RegisterActivity));    // intent for navigating to Register Activity/page
            intent.PutExtra("email",emailEditText.Text);                // pass any email entry data which may've been typed
            StartActivity(intent);                                      // catch PutExtra'd data in GetStringExtra in OnCreate of RegisterActivity
        }

        private async void SignInButton_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            var email = emailEditText.Text;
            var password = passwordEditText.Text;

            var result = await User.Login(email, password);

            if (result)
            {
                Toast.MakeText(this, "Logged in - Welcome", ToastLength.Long).Show();
                Intent intent = new Intent(this, typeof(TabsActivity));
                StartActivity(intent);                      // go to new 'main' page (with tabs)
                Finish();                                   
                // Finish this activity, so as to be no longer available on the stack, and thus un-navigable-to
            }
            else
            {
                Toast.MakeText(this, "Unsuccessful Login - try again", ToastLength.Long).Show();
            }

            /*
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Toast.MakeText(this, "Both email and password must be entered", ToastLength.Long).Show();
            }
            else
            {
                var user = (await MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();      // null if no user in list
                if (user?.Password == password)     // null conditional operator by PNJ
                {
                    Toast.MakeText(this, "Login successful", ToastLength.Long).Show();
                    // navigate to home page
                }
                else
                {
                    Toast.MakeText(this, "Incorrect user name or password", ToastLength.Long).Show();
                    // navigate away if needed
                }
            }
            */
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
	}
}

