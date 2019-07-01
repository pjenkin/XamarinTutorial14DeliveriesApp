using CoreLocation;
using DeliveriesApp.Model;
using Foundation;
using MapKit;
using System;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class NewDeliveryViewController : UIViewController
    {

        // An iOS Location service / location manager cf Android LocationManager
        CLLocationManager locationManager;

        public NewDeliveryViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            locationManager = new CLLocationManager();
            
            locationManager.RequestWhenInUseAuthorization();
            originMapView.ShowsUserLocation = true;
            destinationMapView.ShowsUserLocation = true;

            originMapView.DidUpdateUserLocation += OriginMapView_DidUpdateUserLocation;     // += and TAB for boilerplate event handling code
            destinationMapView.DidUpdateUserLocation += DestinationMapView_DidUpdateUserLocation;

            originMapView.DidFailToLocateUser += OriginMapView_DidFailToLocateUser;


            saveBarButton.Clicked += SaveBarButton_Clicked;     // use += and TAB to boilerplate subscription to event handler
            // NB event on a Bar Button Item is Clicked; whereas on a normal Button tis TouchUpInside
        }

        private void OriginMapView_DidFailToLocateUser(object sender, NSErrorEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void DestinationMapView_DidUpdateUserLocation(object sender, MapKit.MKUserLocationEventArgs e)
        {
            // throw new NotImplementedException();

            // Check whether location set to something
            if (originMapView.UserLocation != null)
            {
                var coordinates = originMapView.UserLocation.Coordinate;        // get center
                var span = new MapKit.MKCoordinateSpan(0.15,0.15);              // how many degrees of lat/lng to show inside the span
                originMapView.Region = new MapKit.MKCoordinateRegion(coordinates, span);

                originMapView.RemoveAnnotations();      // clear all annotations from previous operations from location/map updates
                originMapView.AddAnnotation(new MKPointAnnotation()
                {
                    Coordinate = coordinates,
                    Title = "Your location"
                });
            }
        }

        private void OriginMapView_DidUpdateUserLocation(object sender, MapKit.MKUserLocationEventArgs e)
        {
            // throw new NotImplementedException();

            // Check whether location set to something
            if (destinationMapView.UserLocation != null)
            {
                var coordinates = destinationMapView.UserLocation.Coordinate;        // get center
                var span = new MapKit.MKCoordinateSpan(0.15, 0.15);              // how many degrees of lat/lng to show inside the span
                destinationMapView.Region = new MapKit.MKCoordinateRegion(coordinates, span);

                destinationMapView.RemoveAnnotations();      // clear all annotations from previous operations from location/map updates
                destinationMapView.AddAnnotation(new MKPointAnnotation()
                {
                    Coordinate = coordinates,
                    Title = "My own location"
                });
            }
        }

        private async void SaveBarButton_Clicked(object sender, EventArgs e)
        {
            // throw new NotImplementedException();

            Delivery delivery = new Delivery()
            {
                Name = newPackageNameTextField.Text,
                Status = 0
            };

            await Delivery.InsertDelivery(delivery);        // calling the Delivery object which d'call our Azure helper's generic Insert method
        }
    }
}