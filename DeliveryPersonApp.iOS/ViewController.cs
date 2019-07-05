using DeliveriesApp.Model;
using Foundation;
using System;
using UIKit;

namespace DeliveryPersonApp.iOS
{
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
            deliveryPersonId = await DeliveryPerson.Login(emailTextField.Text, passwordTextField.Text);

            if (string.IsNullOrEmpty(deliveryPersonId))
            {

            }
            else
            {
                hasLoggedIn = true;
                PerformSegue("tabSegue", this); // once signed-in, navigate to TabsActivity with Delivered/Waiting &c tabs
            }
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