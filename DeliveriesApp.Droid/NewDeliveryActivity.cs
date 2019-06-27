using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Model;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "NewDeliveryActivity")]
    //public class NewDeliveryActivity : Activity
    public class NewDeliveryActivity : AppCompatActivity, IOnMapReadyCallback, ILocationListener       // IOnMapReadyCallback needed for .GetMapAsync, ILocationListener for .RequestLocationUpdates
    {

        Button saveNewDeliveryButton;
        EditText packageNameEditText;
        MapFragment mapFragment;
        double latitude, longitude;
        LocationManager locationManager;    // must be initialised from service

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.NewDelivery);

            saveNewDeliveryButton = FindViewById<Button>(Resource.Id.saveNewDeliveryButton);
            packageNameEditText = FindViewById<EditText>(Resource.Id.packageNameEditText);
            mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragment);   // Activity.FragmentManager; fragment ID set in axml element
            // mapFragment.GetMapAsync(this);  // in OnLocationChanged now - activity must implement IOnMapReadyCallback - NB not await'd since a Java object handled by Java, not C#

            saveNewDeliveryButton.Click += SaveNewDeliveryButton_Click;

        }

        // When *activity* is paused/in background, stop running location updates
        protected override void OnPause()
        {
            base.OnPause();
            locationManager.RemoveUpdates(this);        // stop updating when activity in the background
        }

        // Every time *activity* is resumed, get hold of the location provider again if possible
        protected override void OnResume()
        {
            base.OnResume();

            locationManager = GetSystemService(Context.LocationService) as LocationManager;     // initialise locationManager using system
            string provider = LocationManager.GpsProvider;       // choose (NetworkProvider, PassiveProvider,...) GPS for highest accuracy for addresses

            if (locationManager.IsProviderEnabled(provider))    // check whether GPS provider actually available (e.g. device may not even have GPS)
            {
                locationManager.RequestLocationUpdates(provider, 5000, 1000, this);        // keep getting, not just a single fix, at 5s intervals, if 1km or more moved - d'need ILocationListener interface
            }
        }

        private async void SaveNewDeliveryButton_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            Delivery delivery = new Delivery()
            {
                Name = packageNameEditText.Text,
                Status = 0
            };

            await Delivery.InsertDelivery(delivery);
        }

        // Must implement this OnMapReady method for IOnMapReadyCallback interface
        // Set a marker using the device GPS, to show the user's location (for parcel address)
        public void OnMapReady(GoogleMap googleMap)
        {
            MarkerOptions marker = new MarkerOptions();
            marker.SetPosition(new LatLng(latitude, longitude));
            marker.SetTitle("Your own location");
            googleMap.AddMarker(marker);
        }

        public void OnLocationChanged(Location location)
        {
            //throw new NotImplementedException();

            latitude = location.Latitude;
            longitude = location.Longitude;

            mapFragment.GetMapAsync(this);  // activity must implement IOnMapReadyCallback - NB not await'd since a Java object handled by Java, not C#
        }

        public void OnProviderDisabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            //throw new NotImplementedException();
        }
    }
}