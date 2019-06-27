using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
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
    public class NewDeliveryActivity : AppCompatActivity, IOnMapReadyCallback       // IOnMapReadyCallback needed for .GetMapAsync
    {

        Button saveNewDeliveryButton;
        EditText packageNameEditText;
        MapFragment mapFragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.NewDelivery);

            saveNewDeliveryButton = FindViewById<Button>(Resource.Id.saveNewDeliveryButton);
            packageNameEditText = FindViewById<EditText>(Resource.Id.packageNameEditText);
            mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragment);   // Activity.FragmentManager; fragment ID set in axml element
            mapFragment.GetMapAsync(this);  // activity must implement IOnMapReadyCallback - NB not await'd since a Java object handled by Java, not C#

            saveNewDeliveryButton.Click += SaveNewDeliveryButton_Click;
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

        // Must implement this OnMapReady method (even if empty) for IOnMapReadyCallback interface
        public void OnMapReady(GoogleMap googleMap)
        {

        }
    }
}