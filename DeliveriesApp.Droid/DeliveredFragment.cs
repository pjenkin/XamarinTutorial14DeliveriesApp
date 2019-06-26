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

namespace DeliveriesApp.Droid
{
    //public class DeliveredFragment : Fragment
    // public class DeliveredFragment : Android.Support.V4.App.Fragment       // derive from support v4 for backward-compatibility
    public class DeliveredFragment : Android.Support.V4.App.ListFragment       // derive from support v4 for backward-compatibility
    {
        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            // PNJ - ok I will then! :)
            var delivered = await Delivery.GetDelivered();        // get list of delivered packages

            // ListAdapter a member of (Fragment's inherited-from base class) ListFragment, for correct type of list resource to display
            ListAdapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, delivered);
            // NB Activity for context, not 'this'
            // NB var not needed here for ListAdapter (why not?)
        }

        // Because this class is derived from ListFragment, it will contain a ListView, so there's no need to inflate a Fragment

        /*
                public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
                {
                    // Use this to return your custom view for this Fragment
                    return inflater.Inflate(Resource.Layout.Delivered, container, false);

                    return base.OnCreateView(inflater, container, savedInstanceState);
                }
        */
    }
}