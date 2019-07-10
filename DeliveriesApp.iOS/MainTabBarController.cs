using Foundation;
using Plugin.InAppBilling;
using System;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class MainTabBarController : UITabBarController
    {
        public MainTabBarController (IntPtr handle) : base (handle)
        {
            subscriptionBarButtonItem.Clicked += SubscriptionBarButtonItem_Clicked;
        }


        // In-App AppStore purchase
        private async void SubscriptionBarButtonItem_Clicked(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
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

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            // Hide the back button to prevent back-navigation just after logging-in (straightaway, w/o animation)

            NavigationItem.SetHidesBackButton(true, false);
        }
    }
}