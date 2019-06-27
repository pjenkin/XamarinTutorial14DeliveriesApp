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
    [Register ("DeliveredTableViewCell")]
    partial class DeliveredTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        //UIKit.UILabel coordinateLabel { get; set; }
        public UIKit.UILabel coordinateLabel { get; set; }      // public

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        //UIKit.UILabel nameLabel { get; set; }
        public UIKit.UILabel nameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        //UIKit.UILabel statusLabel { get; set; }
        public UIKit.UILabel statusLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (coordinateLabel != null) {
                coordinateLabel.Dispose ();
                coordinateLabel = null;
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