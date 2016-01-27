using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using Spring_Time.APIJsonModels;

namespace Spring_Time
{
    public class MainFragment : Fragment
    {
        #region Private Fields
        private List<ForecastInfo> _listItems;
        private ForecastAdapter _forecastAdapter;
        #endregion

        #region Constructors
        public MainFragment()
        {
            
        }
        #endregion

        #region Android Lifecycle Methods
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

            // view location on the map
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

            _listItems = new List<ForecastInfo>();

            //SetForecastInfo();
            // init the array adapter for the list items
            _forecastAdapter = new ForecastAdapter(Activity, _listItems);
            // find the list view and set it to the forecastAdapter items
            var forecastList = (ListView) rootView.FindViewById(Resource.Id.listViewForecast);
            forecastList.Adapter = _forecastAdapter;

            forecastList.ItemClick += ListItemOnClick;
            
            return rootView;
        }
        #endregion

        #region Private Methods                
        /// <summary>
        /// Lists the item on click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="itemClickEventArgs">The <see cref="AdapterView.ItemClickEventArgs"/> instance containing the event data.</param>
        private void ListItemOnClick(object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            var item = _forecastAdapter.GetItem(itemClickEventArgs.Position);
            var detailIntent = new Intent(Activity, typeof(DetailActivity));
            detailIntent.PutExtra(Intents.ExtraText, item.ToString());

            StartActivity(detailIntent);
        }

        /// <summary>
        /// Sets the forecast information.
        /// </summary>
        private async void SetForecastInfo()
        {
            // get the weather task ready to get task for 7 das
            var task = new FetchWeatherTask(7);

            // get the location and units preferences for this data
            var prefs = PreferenceManager.GetDefaultSharedPreferences(Activity);
            var location = prefs.GetString(GetString(Resource.String.LocationPrefKey), GetString(Resource.String.LocationPrefDefault));

            // get the latest weather forecast
            await task.ExecuteAsync(location);

            if (task.IsSuccessful)
            {
                var data = task.ForecastData;
                _listItems = data.Forecast;
                
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
                alert.SetButton("OK", (sender, args) => { });

                alert.Show();
            }

        }

        #endregion

    }
}