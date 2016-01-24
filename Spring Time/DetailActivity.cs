using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Uri = Android.Net.Uri;

namespace Spring_Time
{
    [Activity(Label = "Details", ParentActivity = typeof(MainActivity))]
    public class DetailActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ActivityDetail);

            if (savedInstanceState == null)
            {
                FragmentManager.BeginTransaction().Add(Resource.Id.container, new  DetailFragment()).Commit();
            }
        }
    }
}