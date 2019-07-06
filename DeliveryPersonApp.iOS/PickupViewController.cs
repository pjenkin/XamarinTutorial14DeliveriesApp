using CoreLocation;
using DeliveriesApp.Model;
using Foundation;
using MapKit;
using System;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class PickupViewController : UIViewController
    {
        public Delivery delivery;
        public string deliveryPersonId;     // this id variable has been declared in & passed on to very many classes for many views - so far at the end of a login->tabs->waiting->pickup chain here
        CLLocationManager locationManager;

        public PickupViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            pickupBarButtonItem.Clicked += PickupBarButtonItem_Clicked;

            PrepareMap();
        }

        private void PrepareMap()
        {
            // throw new NotImplementedException();     // boilerplate from ALT+ENTER on PrepareMap();

            locationManager = new CLLocationManager();
            locationManager.RequestWhenInUseAuthorization();
            pickupMapView.ShowsUserLocation = true;

            var span = new MKCoordinateSpan(0.15, 0.15);        // 0.15 degrees of lat/lng coverage in span
            var coordinates = new CLLocationCoordinate2D(delivery.DestinationLatitude, delivery.DestinationLongitude);

            pickupMapView.Region = new MKCoordinateRegion(coordinates, span);

            pickupMapView.AddAnnotation(new MKPointAnnotation()
            {
                Coordinate = coordinates,
                Title = "Pick up here"      // Coordinate and Title are standard members/fields for MKPointAnnotation
            });
        }

        private async void PickupBarButtonItem_Clicked(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            await Delivery.MarkAsPickedUp(delivery, deliveryPersonId);      // inform db that this delivery person picked up this delivery package
        }

   
    }
}