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

namespace Spring_Time
{
    public class ViewHolder : Java.Lang.Object
    {
        public readonly ImageView IconView;
        public readonly TextView DateView;
        public readonly TextView DescriptionView;
        public readonly TextView HighTempView;
        public readonly TextView LowTempView;

        public ViewHolder(View view)
        {
            IconView = view.FindViewById<ImageView>(Resource.Id.listItemIcon);
            DateView = view.FindViewById<TextView>(Resource.Id.listItemDateTextView);
            DescriptionView = view.FindViewById<TextView>(Resource.Id.listItemForecastTextView);
            HighTempView = view.FindViewById<TextView>(Resource.Id.listItemHighTextView);
            LowTempView = view.FindViewById<TextView>(Resource.Id.listItemLowTextView);
        }
    }
}