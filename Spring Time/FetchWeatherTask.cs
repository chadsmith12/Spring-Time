using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Org.Apache.Http.Client.Params;
using RestSharp;
using Spring_Time.APIJsonModels;


namespace Spring_Time
{
    class FetchWeatherTask
    {
        #region Fields
        private readonly string APPID = "1f19e98ca07882ea323e004b0081b422";
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the count for the forecast search.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; } = 1;

        /// <summary>
        /// Gets the request URL.
        /// </summary>
        /// <value>
        /// The request URL.
        /// </value>
        public string RequestUrl { get; private set; } = "http://api.openweathermap.org/data/2.5/forecast/daily";

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        public string Mode { get; set; } = "JSON";

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the forecast data.
        /// </summary>
        /// <value>
        /// The forecast data.
        /// </value>
        public WeatherForecastApiModel ForecastData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is successful.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is successful; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Gets the response error, if there request was not successful.
        /// </summary>
        /// <value>
        /// The response error if the request was not successful
        /// null if successful
        /// </value>
        public ResponseError ResponseError { get; private set; }

        #endregion

        #region Constructor

        public FetchWeatherTask()
        {

        }

        public FetchWeatherTask(int numberDays)
        {
            Count = numberDays;
        }

        public FetchWeatherTask(string mode)
        {
            Mode = mode;
        }

        public FetchWeatherTask(int numberDays, string mode)
        {
            Count = numberDays;
            Mode = mode;
        }
        #endregion

        #region Methods

        public async Task ExecuteAsync(string query)
        {
            var request = new RestRequest(Method.GET);
            IRestResponse<WeatherForecastApiModel> response = null;
            try
            {
                var client = new RestClient(new Uri(RequestUrl));
                
                request.AddQueryParameter("APPID", APPID);
                request.AddQueryParameter("q", query);
                request.AddQueryParameter("mode", Mode);
                request.AddQueryParameter("cnt", Count.ToString());
                request.AddQueryParameter("units", "metric");

                response = await client.ExecuteGetTaskAsync<WeatherForecastApiModel>(request);


                // received our data and parsed it. Set the data and successful
                if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
                {
                    ForecastData = response.Data;
                    IsSuccessful = true;

                }
                // could not receive our data or parse it. Log the error, and set the error
                else
                {
                    Log.Error("SpringTime", response.ErrorMessage);
                    IsSuccessful = false;
                    ResponseError = new ResponseError(response.StatusCode.ToString(), response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Log.Error("SpringTime", ex.Message);
                IsSuccessful = false;
                ResponseError = response != null ? new ResponseError(response.StatusCode.ToString(), response.ErrorMessage) : new ResponseError("400", ex.Message);
            }
        }

        public void Execute(string query)
        {
            var request = new RestRequest(Method.GET);
            IRestResponse<WeatherForecastApiModel> response = null;
            try
            {
                var client = new RestClient(new Uri(RequestUrl));

                request.AddQueryParameter("APPID", APPID);
                request.AddQueryParameter("q", query);
                request.AddQueryParameter("mode", Mode);
                request.AddQueryParameter("cnt", Count.ToString());
                request.AddQueryParameter("units", "metric");

                response = client.Execute<WeatherForecastApiModel>(request);


                // received our data and parsed it. Set the data and successful
                if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
                {
                    ForecastData = response.Data;
                    IsSuccessful = true;

                }
                // could not receive our data or parse it. Log the error, and set the error
                else
                {
                    Log.Error("SpringTime", response.ErrorMessage);
                    IsSuccessful = false;
                    ResponseError = new ResponseError(response.StatusCode.ToString(), response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Log.Error("SpringTime", ex.Message);
                IsSuccessful = false;
                ResponseError = response != null ? new ResponseError(response.StatusCode.ToString(), response.ErrorMessage) : new ResponseError("400", ex.Message);
            }
        }

        #endregion
    }
}