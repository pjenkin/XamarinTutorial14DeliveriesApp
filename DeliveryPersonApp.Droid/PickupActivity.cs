using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Model;

namespace DeliveryPersonApp.Droid
{
    [Activity(Label = "PickupActivity")]
    public class PickupActivity : Activity, IOnMapReadyCallback     // IOnMapReadyCallback needed by GetMapAsync
    {
        MapFragment mapFragment;
        Button pickupButton;
        double latitude, longitude;
        string deliveryId, deliveryPersonId;

        public void OnMapReady(GoogleMap googleMap)
        {
            // throw new NotImplementedException();
            MarkerOptions marker = new MarkerOptions();
            var destinationLatLng = new LatLng(latitude, longitude);
            marker.SetPosition(destinationLatLng);
            marker.SetTitle("Pick up here");
            googleMap.AddMarker(marker);
            googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(destinationLatLng, 12));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Pickup);     // use the bespoke Pickup layout to show details of pickup arrangements

            mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.pickupMapFragment);
            pickupButton = FindViewById<Button>(Resource.Id.pickupButton);

            latitude = Intent.GetDoubleExtra("latitude", 0);       // Intent is a member of this Activity
            longitude = Intent.GetDoubleExtra("longitude", 0);      // Intent is a member of this Activity
            deliveryId = Intent.GetStringExtra("deliveryId");
            deliveryPersonId = Intent.GetStringExtra("deliveryPersonId");   // catch the PutExtra'd values

            pickupButton.Click += PickupButton_Click;               // +=TAB for handler as usual

            mapFragment.GetMapAsync(this);          // this d'need IOnMapReadyCallback, but no await needed as handled by Java
        }

        private async void PickupButton_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            await Delivery.MarkAsPickedUp(deliveryId, deliveryPersonId);
        }
    }
}