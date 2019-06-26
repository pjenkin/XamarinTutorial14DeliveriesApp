using DeliveriesApp.Model;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class DeliveredViewController : UITableViewController
    {
        List<Delivery> delivered;

        public DeliveredViewController (IntPtr handle) : base (handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            delivered = await Delivery.GetDelivered();
        }
    }
}