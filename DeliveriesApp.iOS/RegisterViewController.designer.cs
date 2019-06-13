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
    [Register ("RegisterViewController")]
    partial class RegisterViewController
    {

        public string emailData;           // to receive data from email text field via a segue - public to be accessible from origin VC
        // Ought this to be all initial caps EmailData since a public member??

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField emailTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField passwordTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (emailTextField != null) {
                emailTextField.Dispose ();
                emailTextField = null;
            }

            if (passwordTextField != null) {
                passwordTextField.Dispose ();
                passwordTextField = null;
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            emailTextField.Text = emailData;        // use data passed from segue
        }
    }
}