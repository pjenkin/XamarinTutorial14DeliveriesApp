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
        public string deliveryPersonId;

        public DeliveringTableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            deliveries = new List<Delivery>();      // initialise member data (zero deliveries to start with)
        }

        // Manual override of ViewDidAppear to call status & other data from Delivery
        public async override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            deliveries = await Delivery.GetBeingDelivered(deliveryPersonId);        // get data for this delivery person, to show in table
            TableView.ReloadData();
        }


        // 2 main overrides for TableViewVCs - rowsInSection and GetCell

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            // return base.RowsInSection(tableView, section);
            return deliveries.Count;        // as many rows in the table as there are relevant delivery records (delivering ie being delivered)
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // For each row..

            var cell = tableView.DequeueReusableCell("deliveringCell");     // reuse custom cell (by its Identifier property values)

            var delivery = deliveries[indexPath.Row];                       // refer to member data for deliveries by this row number

            cell.TextLabel.Text = delivery.Name;

            cell.DetailTextLabel.Text = $"{delivery.DestinationLatitude}, {delivery.DestinationLongitude}";

            return cell;

            // return base.GetCell(tableView, indexPath);
        }


        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "deliverSegue")
            {
                var selectedRow = TableView.IndexPathForSelectedRow;                // v important - get selected row index value (0,...)
                //var destinationViewController = segue.DestinationViewController as PickupViewController;
                var destinationViewController = segue.DestinationViewController as DeliverViewController;
                destinationViewController.delivery = deliveries[selectedRow.Row];   // use the index to fetch selected data and prime upcoming ViewController

                // May have additional sections, but in this case only 1
            }
            base.PrepareForSegue(segue, sender);
        }
    }
}