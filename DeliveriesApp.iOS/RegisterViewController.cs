using Foundation;
using System;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class RegisterViewController : UIViewController
    {
        public string emailData;           // to receive data from email text field

        public RegisterViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            emailTextField.Text = emailData;

            //registerButton.TouchUpInside += RegisterButton_TouchUpInside;      // add a handler for the event and manually define this
            registerButton.TouchUpInside += RegisterButton_TouchUpInside;
            // NB += and TAB will suggest a handler method name and automatically add a boilerplate signature for that method
        }

        // this handler method boilerplate added automatically when TAB'ing after += to add event handler
        private async void RegisterButton_TouchUpInside(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            if (!string.IsNullOrEmpty(passwordTextField.Text))
            {
                if (passwordTextField.Text == confirmPasswordTextField.Text)
                {
                    var user = new User()
                    {
                        Email = emailTextField.Text,
                        Password = passwordTextField.Text,
                    };

                    await AppDelegate.MobileService.GetTable<User>().InsertAsync(user);        // insert record to Azure db table

                    var alert = UIAlertController.Create("Success", "User record successfully added", UIAlertControllerStyle.Alert);

                    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                    PresentViewController(alert, true, null);

                    return;
                }
            }
        }        
    }
}