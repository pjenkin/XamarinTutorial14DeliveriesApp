using DeliveriesApp.Model;
using Foundation;
using System;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class PickupViewController : UIViewController
    {
        public Delivery delivery;

        public PickupViewController (IntPtr handle) : base (handle)
        {
        }
    }
}