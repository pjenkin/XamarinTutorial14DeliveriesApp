using DeliveriesApp.Model;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class WaitingTableViewController : UITableViewController
    {
        List<Delivery> deliveries;
        public WaitingTableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            deliveries = new List<Delivery>();      // initialise member data (no deliveries to start with)
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            if (segue.Identifier == "pickupSegue")
            {
                var selectedRow = TableView.IndexPathForSelectedRow;                // v important - get selected row index value (0,...)
                var destinationViewController = segue.DestinationViewController as DeliverViewController;
                destinationViewController.delivery = deliveries[selectedRow.Row];   // use the index to fetch selected data and prime upcoming ViewController
                // May have additional sections, but in this case only 1
            }
            base.PrepareForSegue(segue, sender);
        }
    }
}