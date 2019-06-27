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
            TableView.ReloadData();      // after new data, refresh table (this method available because subclassing UI*Table*viewController)
        }

        // NB The 2 basic overrides for iOS cell population in TableViews: RowsInSection & GetCell

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            //return base.RowsInSection(tableView, section);       // boilerplate

            // Xamarin weirdness - code bouncing around this line & nulls for deliveries variable from Delivery.GetDeliveries
            return deliveries?.Count ?? 0;      // as many rows as there are delivery records; Return 0 if 'deliveries' is null
            
            //return deliveries.Count;            // line in video 22-167 - as many rows as there are delivery records

            // NB Only 1 section in this app’s case. Otherwise evaluate which section then calculate #rows.
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {

            //var cell = tableView.DequeueReusableCell("deliveryCell");     // recycle this 1 or more cells when fallen out of sight - Identifier value of cell in TableViewController
            var cell = tableView.DequeueReusableCell("deliveryCell") as DeliveryTableViewCell;     // recycle this 1 or more cells when fallen out of sight - Identifier value of cell in TableViewController - cast as custom cell type

            var deliveryValue = deliveries[indexPath.Row];                   // get this row's data for the cell

            //cell.TextLabel.Text = deliveryValue.Name;
            cell.nameLabel.Text = deliveryValue.Name;
            cell.coordinatesLabel.Text = $"{deliveryValue.DestinationLatitude}, {deliveryValue.DestinationLongitude}";

            // cf DeliveriesAdaptor in Android
            switch (deliveryValue.Status)
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

            // DetailTextLabel was available because TableView cell style is 'Subtitle'

            return cell;
            
            //return base.GetCell(tableView, indexPath);

        }

        // Make the cell height enough to hold labels comfortably
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            // return base.GetHeightForRow(tableView, indexPath);
            return 60;      // 60 pixels apparently sufficient in this case
        }
    }
}