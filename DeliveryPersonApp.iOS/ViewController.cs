using DeliveriesApp.Model;
using Foundation;
using LocalAuthentication;
using System;
using UIKit;

namespace DeliveryPersonApp.iOS
{

    // This is the 'entry' or 'landing' point of the app - a sign-in page for persons already registered
    public partial class ViewController : UIViewController
    {
        bool hasLoggedIn = false;
        string deliveryPersonId = string.Empty;

        public ViewController (IntPtr handle) : base (handle)
        {
            ////signInButton.TouchUpInside += SignInButton_TouchUpInside;
            // registerSegueButton.TouchUpInside += RegisterSegueButton_TouchUpInside;
            // RegisterSegueButton_TouchUpInside not actually needed as navigation handled adequately in segue
        }

        /*
         * // RegisterSegueButton_TouchUpInside not actually needed as navigation handled adequately in segue
        private void RegisterSegueButton_TouchUpInside(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
        }
        */
        private async void SignInButton_TouchUpInside(object sender, EventArgs e)
        {
            // throw new NotImplementedException();


            bool success = CheckLogin();
            var something = 0;

            if (success)        // if previously logged-in to server (according to device UserDefault data)
            {
                BiometricsAuth();       // check local biometrics authentication
            }
            else
            {
                // If not recorded on device as having logged-in to server
                ServerLogin();          // check credentials on server db
            }
        }

        /// <summary>
        /// Method to log in users by querying Azure db
        /// </summary>
        private async void ServerLogin()
        {
            deliveryPersonId = await DeliveryPerson.Login(emailTextField.Text, passwordTextField.Text);

            if (string.IsNullOrEmpty(deliveryPersonId))
            {
                BiometricsAuth();           // bespoke method here
            }
            else
            {
                NSUserDefaults.StandardUserDefaults.SetString(deliveryPersonId, "deliveryPersonId");
                NSUserDefaults.StandardUserDefaults.Synchronize();          // save/synchronise the log-in data
                hasLoggedIn = true;
                PerformSegue("tabSegue", this); // once signed-in, navigate to TabsActivity with Delivered/Waiting &c tabs
            }
        }

        /// <summary>
        /// Bespoke method to use touch ID (or iPhone 10 FaceID)
        /// </summary>
        private void BiometricsAuth()
        {
            // throw new NotImplementedException();

            NSError error;

            var context = new LAContext();        // LA Local Authentication - must declare a context for authentication (d'tell us about device capabilities &c)
            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out error))    // can current device use biometrics?
            {
                // NB Local Authentication MUST be performed on the app's main thread https://developer.apple.com/documentation/localauthentication/logging_a_user_into_your_app_with_face_id_or_touch_id
                // Anonymous function defined for local authentication, which must be async to await EvaluatePolicyAsync
                InvokeOnMainThread(async () =>
                {
                    var result = await context.EvaluatePolicyAsync(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, "Login");

                    if (result.Item1)        // if on-device local biometric authentication has OK'd the user (get tuple's Item1 https://docs.microsoft.com/en-us/dotnet/api/system.tuple-1.item1?view=netframework-4.8)
                    {
                        hasLoggedIn = true;
                        PerformSegue("tabSegue", this); // once signed-in, navigate to TabsActivity with Delivered/Waiting &c tabs
                    }
                    else
                    {
                        ServerLogin();  // if user not OK'd biometrically, check credentials on server
                    }
                });
            }
            else
            {
                // If biometric local authentication not available, check credentials on server db
                ServerLogin();
            }
        }

        /// <summary>
        /// Check whether a person is logged in according to iOS device's UserDefaults persistent data
        /// </summary>
        /// <returns></returns>
        private bool CheckLogin()
        {
            // throw new NotImplementedException();

            bool hasId = false;

            deliveryPersonId = NSUserDefaults.StandardUserDefaults.StringForKey("deliveryPersonId");
            // Look up the iOS UserDefaults persistent memory (login will have been saved therein if used) of id - if any

            if (!string.IsNullOrEmpty(deliveryPersonId))
            {
                hasId = true;
            }

            return hasId;
        }

        // Manually override PrepareForSegue
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "tabSegue")     // NB parameters & patterns: segue.Identifier in PrepareForSegue, but segueIdentifier in ShouldPerformSegue
            {
                // NB cast, so as to pass on & set a member variable (deliveryPersonId) in the next view - the Destination ViewController (MainTabBarController)
                var destinationVC = segue.DestinationViewController as MainTabBarController;        
                destinationVC.deliveryPersonId = deliveryPersonId;
            }

                base.PrepareForSegue(segue, sender);
        }

        // Manually override ShouldPerformSegue
        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
        {
            if (segueIdentifier == "tabSegue")
            {
                return hasLoggedIn;
            }
 
            return base.ShouldPerformSegue(segueIdentifier, sender);
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.

            signInButton.TouchUpInside += SignInButton_TouchUpInside;
            // registerSegueButton.TouchUpInside += RegisterSegueButton_TouchUpInside;
            // RegisterSegueButton_TouchUpInside not actually needed as navigation handled adequately in segue

        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}