using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Spring_Time
{
    [Activity(Label = "Spring Time", MainLauncher = true, Icon = "@drawable/sw_icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            if (bundle == null)
            {
                var mainFragment = new MainFragment();
                var fragmentTransaction = FragmentManager.BeginTransaction();
                fragmentTransaction.Add(Resource.Id.container, mainFragment);
                fragmentTransaction.Commit();
            }

        }
    }
}

