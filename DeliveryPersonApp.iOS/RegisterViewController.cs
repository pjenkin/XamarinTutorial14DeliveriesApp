using DeliveriesApp.Model;
using Foundation;
using System;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class RegisterViewController : UIViewController
    {
        public RegisterViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            registerButton.TouchUpInside += RegisterButton_TouchUpInside;
        }

        private async void RegisterButton_TouchUpInside(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            bool success = await DeliveryPerson.Register(registerEmailTextField.Text, registerPasswordTextField.Text, registerConfirmPasswordTextField.Text);

            UIAlertController alert = null;

            if (success)
            {
                alert = UIAlertController.Create("Registration Success", "New user has been registered", UIAlertControllerStyle.Alert);
            }
            else
            {
                alert = UIAlertController.Create("Registration Failure", "Something went wrong while trying to register user", UIAlertControllerStyle.Alert);
            }

            // TODO: need exception catching here to feed back problem to user

            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));      // a response action for the user to click
            PresentViewController(alert, true, null);           // show the alert to the user (the delivery person)
        }
    }
}