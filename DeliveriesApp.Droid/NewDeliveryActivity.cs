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
    //public class NewDeliveryActivity : AppCompatActivity, ILocationListener       // IOnMapReadyCallback needed for .GetMapAsync, ILocationListener for .RequestLocationUpdates

    {

        Button saveNewDeliveryButton;
        EditText packageNameEditText;
        MapFragment originMapFragment, destinationMapFragment;
        double latitude, longitude;
        LocationManager locationManager;        // must be initialised from service
        GoogleMap originMap, destinationMap;    // since Map member is absent from MapFragment in 2019

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.NewDelivery);

            saveNewDeliveryButton = FindViewById<Button>(Resource.Id.saveNewDeliveryButton);
            packageNameEditText = FindViewById<EditText>(Resource.Id.packageNameEditText);
            originMapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.originMapFragment);   // Activity.FragmentManager; fragment ID set in axml element
            //originMapFragment.GetMapAsync(this);  // in OnLocationChanged now - activity must implement IOnMapReadyCallback - NB not await'd since a Java object handled by Java, not C#
            destinationMapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.destinationMapFragment);
            //destinationMapFragment.GetMapAsync(this);
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
                locationManager.RequestLocationUpdates(provider, 5000, 1, this);        // keep getting, not just a single fix, at 5s intervals, if 1km or more moved - d'need ILocationListener interface
            }

            // Get initial location & display this in the map(s)
            var location = locationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
            // Get the last saved location - from cell towers (less accurate but more likely to have existed & been saved)
            latitude = location.Latitude;
            longitude = location.Longitude;

            // could refactor this GetMapAsync stuff as used at least twice in class
            //            MyMap originMap = new MyMap();
            //            MyMap destinationMap = new MyMap();
            MyMap originMap = new MyMap();
            MyMap destinationMap = new MyMap();

            originMapFragment.GetMapAsync(this);
            destinationMapFragment.GetMapAsync(this);
//            originMapFragment.GetMapAsync(originMap);   // the argument is the object in which callback OnMapReady is to be triggered
//            destinationMapFragment.GetMapAsync(destinationMap);
        }

        private async void SaveNewDeliveryButton_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            // Get coordinates of centre of map (using the MyMap bespoke class)
            var originLocation = originMap.CameraPosition.Target;
            var destinationLocation = originMap.CameraPosition.Target;
            

            Delivery delivery = new Delivery()
            {
                Name = packageNameEditText.Text,
                Status = 0,
                OriginLatitude = originLocation.Latitude,
                OriginLongitude = originLocation.Longitude,
                DestinationLatitude = destinationLocation.Latitude,
                DestinationLongitude = destinationLocation.Longitude
            };

            await Delivery.InsertDelivery(delivery);
        }

        // Must implement this OnMapReady method for IOnMapReadyCallback interface
        // Set a marker using the device GPS, to show the user's location (for parcel address)
        public void OnMapReady(GoogleMap googleMap)
        {

            if (this.originMap == null)
            {
                this.originMap = googleMap;
            }
            else
            {
                this.destinationMap = googleMap;
            }

//            MyMap originMap = new MyMap();
//            MyMap destinationMap = new MyMap();

            //originMapFragment.GetMapAsync(this);
            //destinationMapFragment.GetMapAsync(this);
//            originMapFragment.GetMapAsync(originMap);   // the argument is the obect in which callback OnMapReady is to be triggered
//            destinationMapFragment.GetMapAsync(destinationMap);



            MarkerOptions marker = new MarkerOptions();
            marker.SetPosition(new LatLng(latitude, longitude));
            marker.SetTitle("Your own location");
            googleMap.AddMarker(marker);

            googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(latitude, longitude), 10));      // use Factory instead of instanting with a constructor
        }

        public void OnLocationChanged(Location location)
        {
            //throw new NotImplementedException();

            latitude = location.Latitude;
            longitude = location.Longitude;

            MyMap originMap = new MyMap();
            MyMap destinationMap = new MyMap();
            originMapFragment.GetMapAsync(this);  // activity must implement IOnMapReadyCallback - NB not await'd since a Java object handled by Java, not C#
            destinationMapFragment.GetMapAsync(this);
//            originMapFragment.GetMapAsync(originMap);  // activity must implement IOnMapReadyCallback - NB not await'd since a Java object handled by Java, not C#
//            destinationMapFragment.GetMapAsync(destinationMap);
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