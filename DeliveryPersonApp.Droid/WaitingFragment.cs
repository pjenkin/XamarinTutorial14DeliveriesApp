using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Model;

namespace DeliveryPersonApp.Droid
{
    // public class WaitingFragment : Fragment
    public class WaitingFragment : Android.Support.V4.App.ListFragment
    {

        List<Delivery> deliveries;
        string deliveryPersonId;

        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

            deliveries = new List<Delivery>();

            // var userId = (Activity as TabsActivity).deliveryPersonId; // use Fragment's inherited Activity to get (deliveryPersonId) member containing data passed to parent TabsActivity
            deliveryPersonId = (Activity as TabsActivity).deliveryPersonId; // use Fragment's inherited Activity to get (deliveryPersonId) member containing (delivery Person 's Id) data passed to parent TabsActivity
            deliveries = await Delivery.GetWaiting();
            ListAdapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, deliveries);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);


        }

        // override this, with List<Delivery> member (deliveries) to make use of ListFragment's clickability
        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);

            var selectedDelivery = deliveries[position];    // reference the user-selected delivery list item

            var deliveryPersonId = (Activity as TabsActivity).deliveryPersonId; // use Fragment's inherited Activity to get (userId) member containing data passed to parent TabsActivity

            // Show where to pick up the delivery of waiting packages
            Intent intent = new Intent(Activity, typeof(PickupActivity));          // make an intent to move to a DeliverActivity page, for to show the delivery (as in ListFragment not Activity, for context use Activity property of ListFragment (this))
            intent.PutExtra("latitude", selectedDelivery.OriginLatitude);
            intent.PutExtra("longitude", selectedDelivery.OriginLongitude);    // pass over lat/lng to next activity (ie *from where* to pick up this package) - use Intent.GetDoubleExtra
            intent.PutExtra("deliveryPersonId", deliveryPersonId);
            intent.PutExtra("deliveryId", selectedDelivery.Id);                      // also pass to next activity (Deliver Activity) the selected delivery's ID
            StartActivity(intent);
        }
    }
}