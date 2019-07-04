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
    // public class DeliveredFragment : Fragment
    public class DeliveredFragment : Android.Support.V4.App.ListFragment
    {
        List<Delivery> deliveries;

        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

            deliveries = new List<Delivery>();

            var deliveryPersonId = (Activity as TabsActivity).deliveryPersonId; // use Fragment's inherited Activity to get (userId) member containing data passed to parent TabsActivity
            deliveries = await Delivery.GetDelivered(deliveryPersonId);
            ListAdapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, deliveries);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}