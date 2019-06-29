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

namespace DeliveriesApp.iOS
{
    [Register ("NewDeliveryViewController")]
    partial class NewDeliveryViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView destinationMapView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField newPackageNameTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView originMapView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem saveBarButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (destinationMapView != null) {
                destinationMapView.Dispose ();
                destinationMapView = null;
            }

            if (newPackageNameTextField != null) {
                newPackageNameTextField.Dispose ();
                newPackageNameTextField = null;
            }

            if (originMapView != null) {
                originMapView.Dispose ();
                originMapView = null;
            }

            if (saveBarButton != null) {
                saveBarButton.Dispose ();
                saveBarButton = null;
            }
        }
    }
}