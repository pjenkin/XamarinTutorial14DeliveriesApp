using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DeliveriesApp.Droid
{
    // Class to help with MapFragments no longer having Map property for to get current coordinates
    // As per https://www.udemy.com/complete-xamarin-developer-course-ios-and-android/learn/lecture/8438130#questions/6303278
    // Should go in a Model folder of some kind in Droid project?
    // Unused as yet by PNJ, as after GetMapAysnc with this as argument, MyMap::OnMapReady 
    // was not called, Map remained null and (possibly in connection to this) a Java.lang 
    // null pointer runtime error occurred, which I couldn't find via breakpoints
    public class MyMap : Java.Lang.Object, IOnMapReadyCallback          // base class before interface
    {

        public GoogleMap Map { get; set; }

        public IntPtr Handle => Handled();

        public void Dispose()
        {

        }

        public void OnMapReady(GoogleMap googleMap)
        {
            //throw new NotImplementedException();

            Map = googleMap;
        }

        private IntPtr Handled()
        {
            return IntPtr.Zero;
        }
    }
}