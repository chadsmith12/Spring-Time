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

namespace Spring_Time.Resources
{
    static class Converter
    {
        public static double CelsiusToFahrenheit(double celcius)
        {
            return ((9.0/5.0)*celcius) + 32;
        }

        public static double FahrenheightToCelcius(double fahrenheit)
        {
            return (5.0/9.0)*(fahrenheit - 32);
        }
    }
}