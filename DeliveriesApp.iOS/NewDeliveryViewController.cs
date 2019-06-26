using DeliveriesApp.Model;
using Foundation;
using System;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class NewDeliveryViewController : UIViewController
    {
        public NewDeliveryViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            saveBarButton.Clicked += SaveBarButton_Clicked;     // use += and TAB to boilerplate subscription to event handler
            // NB event on a Bar Button Item is Clicked; whereas on a normal Button tis TouchUpInside
        }

        private async void SaveBarButton_Clicked(object sender, EventArgs e)
        {
            // throw new NotImplementedException();

            Delivery delivery = new Delivery()
            {
                Name = newPackageNameTextField.Text,
                Status = 0
            };

            await Delivery.InsertDelivery(delivery);        // calling the Delivery object which d'call our Azure helper's generic Insert method
        }
    }
}