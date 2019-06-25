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
    // public class DeliveriesFragment : Fragment
    public class DeliveriesFragment : Android.Support.V4.App.Fragment       // derive from support v4 for backward-compatibility
    {
            public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.Deliveries, container, false);
            // Un-comment & amend the above auto-commented boilerplate to inflate our Deliveries fragment layout

            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}