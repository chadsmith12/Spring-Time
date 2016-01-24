using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Uri = Android.Net.Uri;

namespace Spring_Time
{
    static class Intents
    {
        public const string ExtraText  = "SpringTimeData";

        public static void StartMap(Context context)
        {
            var locationIntent = new Intent(Intent.ActionView);
            var prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            var location = prefs.GetString(context.GetString(Resource.String.LocationPrefKey), context.GetString(Resource.String.LocationPrefDefault));

            var geoLocation = Uri.Parse("geo:0,0?").BuildUpon().AppendQueryParameter("q", location).Build();
            locationIntent.SetData(geoLocation);

            if (locationIntent.ResolveActivity(context.PackageManager) != null)
            {
                context.StartActivity(locationIntent);
            }
            else
            {
                Log.Debug("SpringTime", "Could not open activity with location: " + location);
            }
        }

        public static Intent CreateShareForecastIntent(string text)
        {
            var shareIntent = new Intent(Intent.ActionSend);
            shareIntent.AddFlags(ActivityFlags.ClearWhenTaskReset);
            shareIntent.SetType("text/plain");
            shareIntent.PutExtra(Intent.ExtraText, text);

            return shareIntent;
        }
    }
}