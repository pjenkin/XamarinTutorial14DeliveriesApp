using DeliveriesApp.Model;
using Foundation;
using System;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class PickupViewController : UIViewController
    {
        public Delivery delivery;
        public string deliveryPersonId;     // this id variable has been declared in & passed on to very many classes for many views - so far at the end of a login->tabs->waiting->pickup chain here

        public PickupViewController (IntPtr handle) : base (handle)
        {
            pickupBarButtonItem.Clicked += PickupBarButtonItem_Clicked;
        }

        private async void PickupBarButtonItem_Clicked(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            await Delivery.MarkAsPickedUp(delivery, deliveryPersonId);      // inform db that this delivery person picked up this delivery package
        }
    }
}