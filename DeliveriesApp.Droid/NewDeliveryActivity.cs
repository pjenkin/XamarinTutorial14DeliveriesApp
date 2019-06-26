using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
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
    public class NewDeliveryActivity : AppCompatActivity
    {

        Button saveNewDeliveryButton;
        EditText packageNameEditText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.NewDelivery);

            saveNewDeliveryButton = FindViewById<Button>(Resource.Id.saveNewDeliveryButton);
            packageNameEditText = FindViewById<EditText>(Resource.Id.packageNameEditText);

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
    }
}