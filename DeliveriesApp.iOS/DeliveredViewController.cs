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
            // Because, ab initio, no deliveries will have been made, have to initialise the list so as to be non-null at least even if empty
            delivered = new List<Delivery>();
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            delivered = await Delivery.GetDelivered();
            TableView.ReloadData();      // after new data, refresh table (this method available because subclassing UI*Table*viewController)
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            // return base.RowsInSection(tableView, section);
            // return delivered.Count;

            // Xamarin weirdness - code bouncing around this line & nulls for deliveries variable from Delivery.GetDeliveries
            return delivered?.Count ?? 0;      // as many rows as there are delivery records; Return 0 if 'deliveries' is null

        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {

            //var cell = tableView.DequeueReusableCell("deliveredCell");       // Identifier for cell in TableRowController
            var cell = tableView.DequeueReusableCell("deliveredCell") as DeliveredTableViewCell;       // Identifier for cell in TableRowController - cast as custom cell type

            var deliveredValue = delivered[indexPath.Row];                  // get this row's value

            //cell.TextLabel.Text = deliveredValue.Name;
            cell.nameLabel.Text = deliveredValue.Name;
            cell.coordinatesLabel.Text = $"{deliveredValue.DestinationLatitude}, {deliveredValue.DestinationLongitude}";

            // cf DeliveriesAdaptor in Android
            switch (deliveredValue.Status)
            {
                case 0:
                    //cell.DetailTextLabel.Text = "Awaiting delivery person";
                    cell.statusLabel.Text = "Awaiting delivery person";
                    break;
                case 1:
                    //cell.DetailTextLabel.Text = "Out for delivery";
                    cell.statusLabel.Text = "Out for delivery";
                    break;
                case 2:
                    //cell.DetailTextLabel.Text = "Already delivered";
                    cell.statusLabel.Text = "Already delivered";
                    break;
                default:
                    //cell.DetailTextLabel.Text = "Delivery status unknown";
                    cell.statusLabel.Text = "Delivery status unknown";
                    break;
            }

            return cell;

            // return base.GetCell(tableView, indexPath);
        }

        // Make the cell height enough to hold labels comfortably
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            // return base.GetHeightForRow(tableView, indexPath);
            return 60;      // 60 pixels apparently sufficient in this case
        }

    }
}