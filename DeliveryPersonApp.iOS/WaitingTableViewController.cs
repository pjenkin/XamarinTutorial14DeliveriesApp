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
        public string deliveryPersonId;

        public WaitingTableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            deliveries = new List<Delivery>();      // initialise member data (no deliveries to start with)
        }

        // Manually override ViewDidAppear
        public async override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            deliveries = await Delivery.GetWaiting();        // get data, to show in table - NB any delivery person could pick up a waiting package, so no deliveryPersonId needed here
            TableView.ReloadData();

        }

        // 2 main overrides for TableViewVCs - rowsInSection and GetCell

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            // return base.RowsInSection(tableView, section);
            return deliveries.Count;        // as many rows in the table as there are relevant delivery records (waiting ie waiting for pickup)
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // For each row..

            var cell = tableView.DequeueReusableCell("waitingCell");     // reuse custom cell (by its Identifier property values)

            var delivery = deliveries[indexPath.Row];                       // refer to member data for deliveries by this row number

            cell.TextLabel.Text = delivery.Name;

            cell.DetailTextLabel.Text = $"{delivery.OriginLatitude}, {delivery.OriginLongitude}";

            return cell;

            // return base.GetCell(tableView, indexPath);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            if (segue.Identifier == "pickupSegue")
            {
                var selectedRow = TableView.IndexPathForSelectedRow;                // v important - get selected row index value (0,...)
                var destinationViewController = segue.DestinationViewController as PickupViewController;
                destinationViewController.delivery = deliveries[selectedRow.Row];   // use the index to fetch selected data and prime upcoming ViewController
                destinationViewController.deliveryPersonId = deliveryPersonId;
                // May have additional sections, but in this case only 1
            }
            base.PrepareForSegue(segue, sender);
        }
    }
}