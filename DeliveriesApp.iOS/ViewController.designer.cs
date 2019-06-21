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
        UIKit.UIButton registerSegueButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton signinButton { get; set; }

        [Action ("RegisterSegueButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void RegisterSegueButton_TouchUpInside (UIKit.UIButton sender);

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

            if (registerSegueButton != null) {
                registerSegueButton.Dispose ();
                registerSegueButton = null;
            }

            if (signinButton != null) {
                signinButton.Dispose ();
                signinButton = null;
            }
        }
    }
}