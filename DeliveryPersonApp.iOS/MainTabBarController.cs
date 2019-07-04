using Foundation;
using System;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class MainTabBarController : UITabBarController
    {
        public MainTabBarController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            NavigationItem.SetHidesBackButton(true, false);     // immediately hide back button, so's to prevent navigation back outside to sign-in page            
        }

    }
}