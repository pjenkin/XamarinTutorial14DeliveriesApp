using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "TabsActivity")]
    //public class TabsActivity : Activity
    public class TabsActivity : FragmentActivity            // must be a FragmentActivity for navigation - SupportFragmentManager
    {
        TabLayout tabLayout;      // refer to our new 'main' layout

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            // ok! :-) This activity will be to do with our Tabs layout (ie the new main page)
            SetContentView(Resource.Layout.Tabs);

            tabLayout = this.FindViewById<TabLayout>(Resource.Id.mainTabLayout);  // get our tab by id defined as android:id="@+id/mainTabLayout"
            tabLayout.TabSelected += TabLayout_TabSelected;                         // += then TAB for auto-complete of event handler

            FragmentNavigate(new DeliveriesFragment());     // in effect, make Deliveries the default fragment on startup
        }

        private void TabLayout_TabSelected(object sender, TabLayout.TabSelectedEventArgs e)
        {
            //throw new NotImplementedException();
            // NB position (ie 1st tabItem, 2nd TabItem, ...), instead of id, needed for determining which tab was selected. Thus in 19-155 no ids defined for Tab Items.
            switch(e.Tab.Position)
            {
                case 0:
                    FragmentNavigate(new DeliveriesFragment());
                    break;
                case 1:
                    FragmentNavigate(new DeliveredFragment());
                    break;
                case 2:
                    FragmentNavigate(new ProfileFragment());
                    break;
            }
        }

        /// bespoke DRY method for navigating to selected fragment layout
        private void FragmentNavigate(Android.Support.V4.App.Fragment fragment)
        {
            var transaction = SupportFragmentManager.BeginTransaction();    // could reset, replace, remove &c fragments
            transaction.Replace(Resource.Id.contentFrame, fragment);
            transaction.Commit();
        }

    }
}