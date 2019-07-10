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
using Plugin.InAppBilling;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "TabsActivity")]
    //public class TabsActivity : Activity
    public class TabsActivity : FragmentActivity            // must be a FragmentActivity for navigation - SupportFragmentManager
    {
        TabLayout tabLayout;                                // refer to our new 'main' layout
        Android.Support.V7.Widget.Toolbar tabsToolbar;      // otherwise from Android.Widget.Toolbar by default - would be different types

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            // ok! :-) This activity will be to do with our Tabs layout (ie the new main page)
            SetContentView(Resource.Layout.Tabs);

            tabsToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.tabsToolbar);       // use the id defined in the element in Tabs.axml

            tabLayout = this.FindViewById<TabLayout>(Resource.Id.mainTabLayout);  // get our tab by id defined as android:id="@+id/mainTabLayout"
            tabLayout.TabSelected += TabLayout_TabSelected;                         // += then TAB for auto-complete of event handler

            tabsToolbar.InflateMenu(Resource.Menu.tabsMenu);                        // use resource defined
            tabsToolbar.MenuItemClick += TabsToolbar_MenuItemClick;     // += and TAB to subscribe with handler method

            FragmentNavigate(new DeliveriesFragment());     // in effect, make Deliveries the default fragment on startup

        }

        private void TabsToolbar_MenuItemClick(object sender, Android.Support.V7.Widget.Toolbar.MenuItemClickEventArgs e)
        {
            // throw new NotImplementedException();
            // Good practice to always check which menu item was selected (even if only 1 item) - use id from resource xml file
            if (e.Item.ItemId == Resource.Id.action_add)
            {
                StartActivity(typeof(NewDeliveryActivity));        // use (2nd) overload requiring type not Intent
            }
            else if (e.Item.ItemId == Resource.Id.action_subscribe)
            {
                Subscribe();
            }

        }

        private async void Subscribe()
        {
            //throw new NotImplementedException();

            // Code copy/pasted from iOS project MainTabBarController::SubscriptionBarButtonItem_Clicked(object sender, EventArgs e)
                try
                {
                    var productId = "";     // if a ProductID has been declared online (App Store Connect: ProductID) , use here
                    var appStoreConnection = await CrossInAppBilling.Current.ConnectAsync();     // connect to AppStore using Plugin.InAppBilling 
                                                                                                 // User will see AppStore dialogues according to available purchase type (e.g. subscription) set up previously

                    if (!appStoreConnection)
                    {
                        return;
                        // TODO show alert that purchase didn't happen (maybe due to network problems)
                    }

                    var purchase = await CrossInAppBilling.Current.PurchaseAsync(productId, Plugin.InAppBilling.Abstractions.ItemType.Subscription, "appPayLoadNotNeededYet");

                    if (purchase == null)       // if something went wrong so there d'seem to have been no purchase
                    {
                        // TODO handle no purchase here
                    }
                    else
                    {
                        // TODO handle results of purchase (unlocking, setting device memory UserDefaults &c) according to returned purchase value
                        var id = purchase.Id;
                        var token = purchase.PurchaseToken;
                        var autoRenewing = purchase.AutoRenewing;
                        var state = purchase.State;         // state could be cancelled, free trial, deferred, paymentpending, failed, uknown, restored, purchased ...
                    }
                }
                catch (Exception exc)
                {
                    // TODO: handle untoward errors here
                }

                finally
                {
                    await CrossInAppBilling.Current.DisconnectAsync();      // important to disconnect from App Store correctly
                }
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