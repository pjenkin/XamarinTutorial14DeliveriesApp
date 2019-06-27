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
    [Register ("DeliveryTableViewCell")]
    partial class DeliveryTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        // UIKit.UILabel coordinatesLabel { get; set; }
        public UIKit.UILabel coordinatesLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        // UIKit.UILabel nameLabel { get; set; }
        public UIKit.UILabel nameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        // UIKit.UILabel statusLabel { get; set; }
        public UIKit.UILabel statusLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (coordinatesLabel != null) {
                coordinatesLabel.Dispose ();
                coordinatesLabel = null;
            }

            if (nameLabel != null) {
                nameLabel.Dispose ();
                nameLabel = null;
            }

            if (statusLabel != null) {
                statusLabel.Dispose ();
                statusLabel = null;
            }
        }
    }
}