using DeliveriesApp.Model;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class DeliveringTableViewController : UITableViewController
    {
        List<Delivery> deliveries;

        public DeliveringTableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            deliveries = new List<Delivery>();      // initialise member data (zero deliveries to start with)
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "deliverSegue")
            {
                var selectedRow = TableView.IndexPathForSelectedRow;                // v important - get selected row index value (0,...)
                var destinationViewController = segue.DestinationViewController as PickupViewController;
                destinationViewController.delivery = deliveries[selectedRow.Row];   // use the index to fetch selected data and prime upcoming ViewController

                // May have additional sections, but in this case only 1
            }
            base.PrepareForSegue(segue, sender);
        }
    }
}