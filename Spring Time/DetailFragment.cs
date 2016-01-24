using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Spring_Time
{
    public class DetailFragment : Fragment
    {
        private string _forecastString;

        public DetailFragment()
        {
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetHasOptionsMenu(true);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var intent = Activity.Intent;
            var rootView = inflater.Inflate(Resource.Layout.DetailFragment, container, false);

            // we started this from clicking a list item
            if (intent != null && intent.HasExtra(Intents.ExtraText))
            {
                _forecastString= intent.GetStringExtra(Intents.ExtraText);

                var detailsText = rootView.FindViewById<TextView>(Resource.Id.detailsTextView);
                detailsText.Text = _forecastString;
            }

            return rootView;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.DetailMenu, menu);

            var menuItem = menu.FindItem(Resource.Id.action_share);

            var shareActionProvider = (ShareActionProvider)menuItem.ActionProvider;

            if (shareActionProvider != null)
            {
                shareActionProvider.SetShareIntent(Intents.CreateShareForecastIntent(_forecastString + " #SpringTimeApp"));
            }
            else
            {
                Log.Debug("SpringTime", "Share Action Provider is null?");
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var id = item.ItemId;

            if (id == Resource.Id.action_settings)
            {
                StartActivity(new Intent(Activity, typeof(SettingsActivity)));
                return true;
            }

            if (id == Resource.Id.action_location)
            {
                Intents.StartMap(Activity);
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}