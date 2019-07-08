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
    [Register ("DeliverViewController")]
    partial class DeliverViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem deliverBarButtonItem { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView deliverMapView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIToolbar deliverMapViewToolbar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem navigateBarButtonItem { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (deliverBarButtonItem != null) {
                deliverBarButtonItem.Dispose ();
                deliverBarButtonItem = null;
            }

            if (deliverMapView != null) {
                deliverMapView.Dispose ();
                deliverMapView = null;
            }

            if (deliverMapViewToolbar != null) {
                deliverMapViewToolbar.Dispose ();
                deliverMapViewToolbar = null;
            }

            if (navigateBarButtonItem != null) {
                navigateBarButtonItem.Dispose ();
                navigateBarButtonItem = null;
            }
        }
    }
}