using Foundation;
using System;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class MainTabBarController : UITabBarController
    {
        public string deliveryPersonId;

        public MainTabBarController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            NavigationItem.SetHidesBackButton(true, false);     // immediately hide back button, so's to prevent navigation back outside to sign-in page            

            var deliveringVC = TabBarController.ViewControllers[0] as DeliveringTableViewController;     // refer to connected tabs of TabBarController in storyboard
            deliveringVC.deliveryPersonId = deliveryPersonId;        // set the next VC's member data now

            var waitingVC = TabBarController.ViewControllers[1] as WaitingTableViewController;     // refer to connected tabs of TabBarController in storyboard
            waitingVC.deliveryPersonId = deliveryPersonId;        // set the next VC's member data now

            var deliveredVC = TabBarController.ViewControllers[2] as DeliveredTableViewController;     // refer to connected tabs of TabBarController in storyboard
            deliveredVC.deliveryPersonId = deliveryPersonId;        // set the next VC's member data now


        }

    }
}