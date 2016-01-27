using System;
using Android.Content;
using Android.Preferences;

namespace Spring_Time
{
    static class Converter
    {
        /// <summary>
        /// Celsiuses to fahrenheit.
        /// </summary>
        /// <param name="celcius">The celcius.</param>
        /// <returns></returns>
        public static double CelsiusToFahrenheit(double celcius)
        {
            return ((9.0/5.0)*celcius) + 32;
        }

        /// <summary>
        /// Fahrenheits to celcius.
        /// </summary>
        /// <param name="fahrenheit">The fahrenheit.</param>
        /// <returns></returns>
        public static double FahrenheitToCelcius(double fahrenheit)
        {
            return (5.0/9.0)*(fahrenheit - 32);
        }
    }

    static class Utilities
    {

        /// <summary>
        /// Gets the friendly date.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetFriendlyDate(DateTime dateTime)
        {
            
            var currentDate = DateTime.Now;

            // building a string for todays date
            if (dateTime.Date == currentDate.Date)
            {
                return $"Today, {dateTime.ToString("MMMM")} {dateTime.Day}";
            }

            // add a day to the current day to see if the date we passed in is tomorrow
            // building a string for tomorrow
            if (dateTime.Date == currentDate.AddDays(1).Date)
            {
                return $"Tomorrow, {dateTime.ToString("MMMM")} {dateTime.Day}";
            }

            // for the next 5 days
            return $"{dateTime.DayOfWeek}, {dateTime.ToString("MMMM")}, {dateTime.Day}";
        }

        /// <summary>
        /// Determines whether the specified context is metric.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static bool IsMetric(Context context)
        {
            // get the location and units preferences for this data
            var prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            var units = prefs.GetString(context.GetString(Resource.String.TempPrefKey), context.GetString(Resource.String.LocationPrefDefault));

            return units.Equals(context.GetString(Resource.String.TempPrefMetric));
        }

        /// <summary>
        /// Formats the temperature.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="temperature">The temperature.</param>
        /// <param name="isMetric">if set to <c>true</c> [is metric].</param>
        /// <returns></returns>
        public static string FormatTemperature(Context context, double temperature, bool isMetric)
        {
            double temp;
            if (!isMetric)
            {
                temp = Converter.CelsiusToFahrenheit(temperature);
            }
            else
            {
                temp = temperature;
            }

            return context.GetString(Resource.String.FormatTemperature, temp);
        }
    }
}