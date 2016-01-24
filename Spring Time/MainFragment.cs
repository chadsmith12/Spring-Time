using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Android.Content.PM;
using Android.Preferences;
using Java.Lang;
using Java.Util;
using Spring_Time.Resources;
using Uri = Android.Net.Uri;

namespace Spring_Time
{
    public class MainFragment : Fragment
    {
        private List<string> _listItems;
        private ArrayAdapter<string> _forecastAdapter;
        public MainFragment()
        {
            
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetHasOptionsMenu(true);
        }

        public override void OnStart()
        {
            base.OnStart();
            SetForecastInfo();
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.ForecastFragmentMenu, menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var id = item.ItemId;

            // refresh
            if (id == Resource.Id.refresh_weather)
            {
                SetForecastInfo();
                
                return true;
            }

            // settings
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

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        { 
            // Use this to return your custom view for this Fragment
            var rootView = inflater.Inflate(Resource.Layout.MainFragment, container, false);

            _listItems = new List<string>();

            // init the array adapter for the list items
            _forecastAdapter = new ArrayAdapter<string>(Activity, Resource.Layout.ListItemLayout, Resource.Id.listItemForecastText, _listItems);
            // find the list view and set it to the forecastAdapter items
            var forecastList = (ListView) rootView.FindViewById(Resource.Id.listViewForecast);
            forecastList.Adapter = _forecastAdapter;

            forecastList.ItemClick += ForecastListOnItemClick;
            
            return rootView;
        }

        private void ForecastListOnItemClick(object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            var item = _forecastAdapter.GetItem(itemClickEventArgs.Position);
            var detailIntent = new Intent(Activity, typeof(DetailActivity));
            detailIntent.PutExtra(Intents.ExtraText, item);

            StartActivity(detailIntent);
        }

        public async Task SetForecastInfo()
        {
            // get the weather task ready to get task for 7 das
            var task = new FetchWeatherTask(7);

            // get the location and units preferences for this data
            var prefs = PreferenceManager.GetDefaultSharedPreferences(Activity);
            var location = prefs.GetString(GetString(Resource.String.LocationPrefKey), GetString(Resource.String.LocationPrefDefault));
            var units = prefs.GetString(GetString(Resource.String.TempPrefKey), GetString(Resource.String.LocationPrefDefault));

            var newData = new List<string>();

            // get the latest weather forecast
            await task.ExecuteAsync(location);

            if (task.IsSuccessful)
            {
                var data = task.ForecastData;
                foreach (var item in data.Forecast)
                {
                    var temperature = item.Temperature.DayTemperature;
                    if (units.Equals(GetString(Resource.String.TempPrefImperial)))
                    {
                        temperature = Converter.CelsiusToFahrenheit(temperature);
                    }

                    newData.Add($"{item.Date.ToString("ddd, MMMM dd")} / {item.Details.Type} / {System.Math.Round(temperature)}");
                }

                // set all the data and set the adapter
                _listItems = newData;
                _forecastAdapter.Clear();
                _forecastAdapter.AddAll(_listItems);
            }
            // task was not successful, let the user know
            else
            {
                var builder = new AlertDialog.Builder(Activity);
                var alert = builder.Create();
                alert.SetTitle("Error: " + task.ResponseError.Code);
                alert.SetMessage("I'm sorry, there was an issue getting the weather: " + task.ResponseError.ErrorMessage);
                alert.SetButton("OK", (sender, args) => {});

                alert.Show();
            }
            
        }

    }
}