using CoreLocation;
using DeliveriesApp.Model;
using Foundation;
using MapKit;
using System;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class DeliverViewController : UIViewController
    {
        public Delivery delivery;
        CLLocationManager locationManager;      // needed for mapping points
        // don't need to know deliveryPersonId here as this isn't required knowledge for marking that a package has been delivered

        public DeliverViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            deliverBarButtonItem.Clicked += DeliverBarButtonItem_Clicked;

            PrepareMap();
        }

        private void PrepareMap()
        {
            // throw new NotImplementedException();     // boilerplate from ALT+ENTER on PrepareMap();

            locationManager = new CLLocationManager();
            locationManager.RequestWhenInUseAuthorization();
            deliverMapView.ShowsUserLocation = true;

            var span = new MKCoordinateSpan(0.15, 0.15);        // 0.15 degrees of lat/lng coverage in span
            var coordinates = new CLLocationCoordinate2D(delivery.DestinationLatitude, delivery.DestinationLongitude);

            deliverMapView.Region = new MKCoordinateRegion(coordinates, span);

            deliverMapView.AddAnnotation(new MKPointAnnotation()
            {
                Coordinate = coordinates,
                Title = "Deliver here"      // Coordinate and Title are standard members/fields for MKPointAnnotation
            });
        }

        private async void DeliverBarButtonItem_Clicked(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            await Delivery.MarkAsDelivered(delivery);     // hooray! package delivered - tell the Azure db
        }
    }
}