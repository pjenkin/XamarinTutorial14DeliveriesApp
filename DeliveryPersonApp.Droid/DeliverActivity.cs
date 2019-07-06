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
    [Activity(Label = "DeliverActivity")]
    public class DeliverActivity : Activity, IOnMapReadyCallback        // IOnMapReadyCallback needed by GetMapAsync
    {
        MapFragment mapFragment;
        Button deliverButton;
        double latitude, longitude;
        string deliveryPersonId, deliveryId;

        public void OnMapReady(GoogleMap googleMap)
        {
            // throw new NotImplementedException();
            MarkerOptions marker = new MarkerOptions();
            var destinationLatLng = new LatLng(latitude, longitude);
            marker.SetPosition(destinationLatLng);
            marker.SetTitle("Deliver here");
            googleMap.AddMarker(marker);
            googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(destinationLatLng, 12));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Deliver);       // show our own Deliver layout

            mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.deliverMapFragment);
            deliverButton = FindViewById<Button>(Resource.Id.deliverButton);

            deliverButton.Click += DeliverButton_Click;

            latitude = Intent.GetDoubleExtra("latitude", 0);       // Intent is a member of this Activity
            longitude = Intent.GetDoubleExtra("longitude", 0);      // Intent is a member of this Activity
            deliveryId = Intent.GetStringExtra("deliveryId");
            deliveryPersonId = Intent.GetStringExtra("deliveryPersonId");       // catch the PutExtra'd values

            mapFragment.GetMapAsync(this);      // no need for await here as await'd by Java; d'need IOnMapReadyCallback
        }

        private async void DeliverButton_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            await Delivery.MarkAsDelivered(deliveryId);               // record this delivery as having been delivereed, using the Activity's string property (value PutExtra'd by Intent from DeliveringFragment)
        }
    }
}