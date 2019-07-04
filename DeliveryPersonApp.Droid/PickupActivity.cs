using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Model;

namespace DeliveryPersonApp.Droid
{
    [Activity(Label = "PickupActivity")]
    public class PickupActivity : Activity
    {
        MapFragment mapFragment;
        Button pickupButton;
        double latitude, longitude;
        string deliveryId, deliveryPersonId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Pickup);     // use the bespoke Pickup layout to show details of pickup arrangements

            mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.pickupMapFragment);
            pickupButton = FindViewById<Button>(Resource.Id.pickupButton);

            latitude = Intent.GetDoubleExtra("latitude", 0);       // Intent is a member of this Activity
            longitude = Intent.GetDoubleExtra("latitude", 0);      // Intent is a member of this Activity
            deliveryId = Intent.GetStringExtra("deliveryId");
            deliveryPersonId = Intent.GetStringExtra("deliveryPersonId");   // catch the PutExtra'd values

            pickupButton.Click += PickupButton_Click;               // +=TAB for handler as usual
        }

        private async void PickupButton_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            await Delivery.MarkAsPickedUp(deliveryId, deliveryPersonId);
        }
    }
}