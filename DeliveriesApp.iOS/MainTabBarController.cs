using Foundation;
using System;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class MainTabBarController : UITabBarController
    {
        public MainTabBarController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            // Hide the back button to prevent back-navigation just after logging-in (straightaway, w/o animation)

            NavigationItem.SetHidesBackButton(true, false);
        }
    }
}