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
    //public class DeliveringFragment : Fragment
    public class DeliveringFragment : Android.Support.V4.App.ListFragment
    {
        List<Delivery> deliveries;

        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

            deliveries = new List<Delivery>();

            var userId = (Activity as TabsActivity).deliveryPersonId; // use Fragment's inherited Activity to get (userId) member containing data passed to parent TabsActivity
            deliveries = await Delivery.GetBeingDelivered(userId);
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

            Intent intent = new Intent(Activity, typeof(DeliverActivity));          // make an intent to move to a DeliverActivity page, for to show the delivery (as in ListFragment not Activity, for context use Activity property of ListFragment (this))
            intent.PutExtra("latitude", selectedDelivery.DestinationLatitude);
            intent.PutExtra("longitude", selectedDelivery.DestinationLongitude);    // pass over the selected delivery's lat/lng to next activity
            intent.PutExtra("deliveryId",selectedDelivery.Id);                      // also pass to next activity (Deliver Activity) the selected delivery's ID
            StartActivity(intent);                                                  // go to next activity
        }
    }
}