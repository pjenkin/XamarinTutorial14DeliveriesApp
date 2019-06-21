using Foundation;
using System;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.
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

    }
}