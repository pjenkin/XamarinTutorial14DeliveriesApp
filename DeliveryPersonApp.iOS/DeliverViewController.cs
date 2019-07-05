using DeliveriesApp.Model;
using Foundation;
using System;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class DeliverViewController : UIViewController
    {
        public Delivery delivery;

        public DeliverViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            deliverBarButtonItem.Clicked += DeliverBarButtonItem_Clicked;
        }

        private async void DeliverBarButtonItem_Clicked(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            await Delivery.MarkAsDelivered(delivery);     // hooray! package delivered - tell the Azure db
        }
    }
}