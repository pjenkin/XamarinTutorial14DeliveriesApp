// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    [Register ("PickupViewController")]
    partial class PickupViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem pickupBarButtonItem { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (pickupBarButtonItem != null) {
                pickupBarButtonItem.Dispose ();
                pickupBarButtonItem = null;
            }
        }
    }
}