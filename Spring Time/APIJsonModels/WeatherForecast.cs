using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestSharp.Deserializers;

namespace Spring_Time.APIJsonModels
{
    /// <summary>
    /// API Json Model to get the response returned by the OpenWeather API
    /// </summary>
    class WeatherForecastApiModel
    {
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [DeserializeAs(Name = "city")]
        public City City { get; set; }

        /// <summary>
        /// Gets or sets the forecast.
        /// </summary>
        /// <value>
        /// The forecast.
        /// </value>
        [DeserializeAs(Name = "list")]
        public List<ForecastInfo> Forecast { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [DeserializeAs(Name = "cod")]
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [DeserializeAs(Name = "message")]
        public string Message { get; set; }
    }

    /// <summary>
    /// The forecast info for a given day
    /// </summary>
    class ForecastInfo
    {
        public ForecastInfo()
        {
            
        }
        /// <summary>
        /// Gets or sets the time of data.
        /// </summary>
        /// <value>
        /// The time of data.
        /// </value>
        [DeserializeAs(Name = "dt")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the weather information.
        /// </summary>
        /// <value>
        /// The weather information.
        /// </value>
        [DeserializeAs(Name = "temp")]
        public Temperature Temperature { get; set; }

        /// <summary>
        /// The details
        /// </summary>
        public Weather Details => Weather.FirstOrDefault();

        /// <summary>
        /// Gets or sets the pressure.
        /// </summary>
        /// <value>
        /// The pressure.
        /// </value>
        [DeserializeAs(Name = "pressure")]
        public double Pressure { get; set; }

        /// <summary>
        /// Gets or sets the humidity.
        /// </summary>
        /// <value>
        /// The humidity.
        /// </value>
        [DeserializeAs(Name = "humidity")]
        public int Humidity { get; set; }

        /// <summary>
        /// Gets or sets the wind speed.
        /// </summary>
        /// <value>
        /// The wind speed.
        /// </value>
        [DeserializeAs(Name = "speed")]
        public double WindSpeed { get; set; }

        /// <summary>
        /// Gets or sets the wind degree.
        /// </summary>
        /// <value>
        /// The wind degree.
        /// </value>
        [DeserializeAs(Name = "deg")]
        public double WindDegree { get; set; }

        /// <summary>
        /// Gets or sets the cloud percentage.
        /// </summary>
        /// <value>
        /// The cloud percentage.
        /// </value>
        [DeserializeAs(Name = "clouds")]
        public int CloudPercentage { get; set; }

        /// <summary>
        /// Gets or sets the weather.
        /// </summary>
        /// <value>
        /// The weather.
        /// </value>
        [DeserializeAs(Name = "weather")]
        public  List<Weather> Weather { get; set; }
    }

    /// <summary>
    /// The morning, day, night, evening, high, low temperature for a day
    /// </summary>
    class Temperature
    {
        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>
        /// The temperature.
        /// </value>
        [DeserializeAs(Name = "day")]
        public double DayTemperature { get; set; }

        /// <summary>
        /// Gets or sets the minimum temperature.
        /// </summary>
        /// <value>
        /// The minimum temperature.
        /// </value>
        [DeserializeAs(Name = "min")]
        public double MinTemperature { get; set; }

        /// <summary>
        /// Gets or sets the maximum temperature.
        /// </summary>
        /// <value>
        /// The maximum temperature.
        /// </value>
        [DeserializeAs(Name = "max")]
        public double MaxTemperature { get; set; }

        /// <summary>
        /// Gets or sets the evening temperature.
        /// </summary>
        /// <value>
        /// The evening temperature.
        /// </value>
        [DeserializeAs(Name = "eve")]
        public double EveningTemperature { get; set; }

        /// <summary>
        /// Gets or sets the night temperature.
        /// </summary>
        /// <value>
        /// The night temperature.
        /// </value>
        [DeserializeAs(Name = "night")]
        public double NightTemperature { get; set; }

        /// <summary>
        /// Gets or sets the morning temperature.
        /// </summary>
        /// <value>
        /// The morning temperature.
        /// </value>
        [DeserializeAs(Name = "morn")]
        public double MorningTemperature { get; set; }
    }

    /// <summary>
    /// Holds the current weather conditions
    /// </summary>
    class Weather
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DeserializeAs(Name = "main")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [DeserializeAs(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        [DeserializeAs(Name = "icon")]
        public string Icon { get; set; }
    }

    class City
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the coordinate.
        /// </summary>
        /// <value>
        /// The coordinate.
        /// </value>
        [DeserializeAs(Name = "coord")]
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [DeserializeAs(Name = "country")]
        public string Country { get; set; }
    }

    class Coordinate
    {
        [DeserializeAs(Name = "lon")]
        public double Longitude { get; set; }
        [DeserializeAs(Name = "lat")]
        public double Latitude { get; set; }
    }

    class Wind
    {
        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public double Speed { get; set; }

        /// <summary>
        /// Gets or sets the degree.
        /// </summary>
        /// <value>
        /// The degree.
        /// </value>
        [DeserializeAs(Name = "deg")]
        public double Degree { get; set; }
    }
}