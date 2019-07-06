using DeliveriesApp.Model;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class DeliveredTableViewController : UITableViewController
    {
        List<Delivery> deliveries;
        public string deliveryPersonId;

        public DeliveredTableViewController (IntPtr handle) : base (handle)
        {
        }

        public async override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            deliveries = await Delivery.GetDelivered(deliveryPersonId);        // get data for this delivery person, to show in table
            TableView.ReloadData();

        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            deliveries = new List<Delivery>();      // initialise member data (zero deliveries to start with)

        }

        // 2 main overrides for TableViewVCs - rowsInSection and GetCell

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            // return base.RowsInSection(tableView, section);
            return deliveries.Count;        // as many rows in the table as there are relevant delivery records (delivered ie having been fully delivered at destination)
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // For each row..

            var cell = tableView.DequeueReusableCell("deliveredCell");     // reuse custom cell (by its Identifier property values)

            var delivery = deliveries[indexPath.Row];                       // refer to member data for deliveries by this row number

            cell.TextLabel.Text = delivery.Name;

            cell.DetailTextLabel.Text = $"{delivery.DestinationLatitude}, {delivery.DestinationLongitude}";

            return cell;

            // return base.GetCell(tableView, indexPath);
        }


        // NB no preparation or checks for segues from Delivered as there is no following screen (delivered package is end-of-story)
    }
}