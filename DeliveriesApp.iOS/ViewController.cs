using Foundation;
using System;
using System.Linq;
using UIKit;
using DeliveriesApp.Model;

namespace DeliveriesApp.iOS
{
    public partial class ViewController : UIViewController
    {

        bool hasLoggedIn = false;       // private, hence camel case

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.

            signinButton.TouchUpInside += SigninButton_TouchUpInside;   // use += with TAB for boilerplate event handler method
        }

        private async void SigninButton_TouchUpInside(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            var email = emailTextField.Text;
            var password = passwordText.Text;
            UIAlertController alert = null;     // no Toast in iOS!

            var result = await User.Login(email, password);

            if(result)
            {
                hasLoggedIn = true;
                alert = UIAlertController.Create("Successful", "Welcome", UIAlertControllerStyle.Alert);
                PerformSegue("loginSegue", this);     // workaround login segue logic execution (alongside ShouldPerformSegue)
            }
            else
            {
                alert = UIAlertController.Create("Failure", "Email or password is incorrect", UIAlertControllerStyle.Alert);
            }


            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            PresentViewController(alert, true, null);       // PresentViewController to make alert show up


            /*
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                alert = UIAlertController.Create("Login incomplete", "Both email and password must be entered", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alert, true, null);  // https://forums.xamarin.com/discussion/140298/uialertcontroller-not-showing
            }
            else
            {
                var user = (await AppDelegate.MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();      // null if no user in list
                if (user?.Password == password)     // PNJ null conditional added
                {
                    //Toast.MakeText(this, "Login successful", ToastLength.Long).Show();
                    alert = UIAlertController.Create("Successful", "Welcome", UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("Thanks", UIAlertActionStyle.Default, null));
                    PresentViewController(alert, true, null);       // PresentViewController to make alert show up

                    // navigate to home page
                }
                else
                {
                    // Toast.MakeText(this, "Incorrect user name or password", ToastLength.Long).Show();
                    alert = UIAlertController.Create("Failure", "Email or password is incorrect", UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                    PresentViewController(alert, true, null);

                    // navigate away if needed
                }
            }

             * */
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }


        partial void RegisterSegueButton_TouchUpInside(UIButton sender)
        {
            // throw new NotImplementedException();

        }

        // NB type 'override' + intellisense will help
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);
            // This code is specifically for the register segue
            if (segue.Identifier == "registerSegue")       // segue Identifier set by storyboard & properties
            {
                var destinationViewController = segue.DestinationViewController as RegisterViewController;
                destinationViewController.emailData = emailTextField.Text;
            }
        }


        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
        {
            //return base.ShouldPerformSegue(segueIdentifier, sender);

            if (segueIdentifier == "loginSegue")
            {
                return hasLoggedIn;
            }

            return true;        // any other segue than a loginSegue, go right ahead and segue (20-159)

            // Hard-coded segue identifier string - could fetch or parameterise this?
        }
    }
}