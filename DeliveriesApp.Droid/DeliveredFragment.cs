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

namespace DeliveriesApp.Droid
{
    //public class DeliveredFragment : Fragment
    // public class DeliveredFragment : Android.Support.V4.App.Fragment       // derive from support v4 for backward-compatibility
    public class DeliveredFragment : Android.Support.V4.App.ListFragment       // derive from support v4 for backward-compatibility
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
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