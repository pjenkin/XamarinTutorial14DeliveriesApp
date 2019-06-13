// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace DeliveriesApp.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField emailTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField passwordText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton registerButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton signinButton { get; set; }

        [Action ("RegisterButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void RegisterButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (emailTextField != null) {
                emailTextField.Dispose ();
                emailTextField = null;
            }

            if (passwordText != null) {
                passwordText.Dispose ();
                passwordText = null;
            }

            if (registerButton != null) {
                registerButton.Dispose ();
                registerButton = null;
            }

            if (signinButton != null) {
                signinButton.Dispose ();
                signinButton = null;
            }
        }

        // NB type 'override' & intellisense will help
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