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

            var deliveriesData = await Delivery.GetDeliveries();

            deliveries = deliveriesData;
            ////deliveries = await Delivery.GetDeliveries();
            TableView.ReloadData();      // after new data, refresh table (this method available because subclassing UI*Table*viewController)
        }

        // NB The 2 basic overrides for iOS cell population in TableViews: RowsInSection & GetCell

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            //return base.RowsInSection(tableView, section);       // boilerplate

            // return 0 if deliveries is null
            return deliveries?.Count ?? 0;
            /*
            return deliveries.Count;
            //return deliveries.Count;            // as many rows as there are delivery records
            */
            // NB Only 1 section in this app’s case. Otherwise evaulate which section then calculate #rows.
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {

            var cell = tableView.DequeueReusableCell("deliveryCell");     // recycle this 1 or more cells when fallen out of sight - Identifier value of cell in TableViewController

            var deliveryValue = deliveries[indexPath.Row];                   // get this row's data for the cell

            cell.TextLabel.Text = deliveryValue.Name;

            // cf DeliveriesAdaptor in Android
            switch (deliveryValue.Status)
            {
                case 0:
                    cell.DetailTextLabel.Text = "Awaiting delivery person";
                    break;
                case 1:
                    cell.DetailTextLabel.Text = "Out for delivery";
                    break;
                case 2:
                    cell.DetailTextLabel.Text = "Already delivered";
                    break;
                default:
                    cell.DetailTextLabel.Text = "Delivery status unknown";
                    break;
            }

            // DetailTextLabel available because TableView cell style is 'Subtitle'

            return cell;
            
            //return base.GetCell(tableView, indexPath);

        }
    }
}