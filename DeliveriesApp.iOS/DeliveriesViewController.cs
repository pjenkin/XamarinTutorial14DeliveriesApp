using DeliveriesApp.Model;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class DeliveriesViewController : UITableViewController
    {

        List<Delivery> deliveries;

        public DeliveriesViewController (IntPtr handle) : base (handle)
        {
        }

        // staple - override ViewDidLoad & therein load up members with data
        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            deliveries = await Delivery.GetDeliveries();
        }
    }
}