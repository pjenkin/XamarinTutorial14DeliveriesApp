using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Model;

namespace DeliveriesApp.Droid
{
    class DeliveryAdapter : BaseAdapter
    {

        Context context;

        List<Delivery> deliveries;              // for use with multiple Delivery records

        //public DeliveryAdapter(Context context)
        public DeliveryAdapter(Context context, List<Delivery> deliveries)      // constructor modified to take our data of interest
        {
            this.context = context;

            this.deliveries = deliveries;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            DeliveryAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as DeliveryAdapterViewHolder;

            if (holder == null)
            {
                holder = new DeliveryAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                //view = inflater.Inflate(Resource.Layout.item, parent, false);

                // Adapt the REMmed-out boilerplate to make use of our bespoke DeliveryCell in ListView's inflation
                view = inflater.Inflate(Resource.Layout.DeliveryCell, parent, false);

                // set the holder's properties (see its modified definition below) to our text fields' values
                holder.Name = view.FindViewById<TextView>(Resource.Id.deliveryNameTextView);
                holder.Status = view.FindViewById<TextView>(Resource.Id.deliveryStatusTextView);

                //holder.Title = view.FindViewById<TextView>(Resource.Id.text);
                view.Tag = holder;
            }


            //fill in your items
            //holder.Title.Text = "new text here";
            var delivery = deliveries[position];         // 'position' from signature of Adapter - ie for every cell, ....
            holder.Name.Text = delivery.Name;           // set the name property of the adapter for the bespoke cell's text field
            switch (delivery.Status)
            {
                case 0:
                    holder.Status.Text = "Awaiting delivery person";
                    break;
                case 1:
                    holder.Status.Text = "Out for delivery";
                    break;
                case 2:
                    holder.Status.Text = "Already delivered";
                    break;
                default:
                    holder.Status.Text = "Delivery status unknown";
                    break;
            }

            return view;
        }

        //Fill in cound here, currently 0
        // (sic) coun*d* :-/ in boilerplate
        /// <summary>
        ///  return number of yet-to-deliver items
        /// </summary>
        public override int Count
        {
            get
            {
                // return 0;
                return deliveries.Count;
            }
        }

    }

    class DeliveryAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }

        // Amend boilerplate holder properties for use in our GetView (to match fields in our bespoke DeliveryCell)
        public TextView Name { get; set; }
        public TextView Status { get; set; }

    }
}