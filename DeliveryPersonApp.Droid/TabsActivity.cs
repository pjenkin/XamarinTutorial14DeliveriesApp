using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace DeliveryPersonApp.Droid
{
    [Activity(Label = "TabsActivity")]
    //public class TabsActivity : Activity
    public class TabsActivity : Android.Support.V4.App.FragmentActivity     // backward-compatible
    {

        TabLayout tabLayout;
        public string deliveryPersonId;              // data to be received via Intent.PutExtra from login activity
        // E Rosas in his withdom decided to call this userId even though this was for a delivery person. Ugg :-/


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Tabs);       // use our own layout - to contain tabs for displaying Delivered, Delivering & Waiting fragments

            deliveryPersonId = Intent.GetStringExtra("deliveryPersonID");   // get Intent.PutExtra'd string

            tabLayout = FindViewById<TabLayout>(Resource.Id.mainTabLayout);
            tabLayout.TabSelected += TabLayout_TabSelected;     // += TAB for boilerplate event handler

            TabNavigation(new DeliveringFragment());      // delivering tab set to be the initial view
        }

        // Keep things DRY and separated concerns
        private void TabLayout_TabSelected(object sender, TabLayout.TabSelectedEventArgs e)
        {
            // throw new NotImplementedException();
            switch(e.Tab.Position)
            {
                case 0:
                    TabNavigation(new DeliveringFragment());        // argument must be a ListFragment
                    break;
                case 1:
                    TabNavigation(new WaitingFragment());        // argument must be a ListFragment
                    break;
                case 2:
                    TabNavigation(new DeliveredFragment());        // argument must be a ListFragment
                    break;
                default:
                    TabNavigation(new DeliveringFragment());      // delivering tab set to be the initial view
                    break;
            }
        }

        private void TabNavigation(Android.Support.V4.App.Fragment fragment)
        {
            // Replace the tab/fragment content according to tab selection

            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.contentFrame, fragment);
            transaction.Commit();
        }
    }
}