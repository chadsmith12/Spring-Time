using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Spring_Time.APIJsonModels;

namespace Spring_Time
{
    class ForecastAdapter : BaseAdapter<ForecastInfo>
    {
        private readonly List<ForecastInfo> _forecast;
        private readonly Activity _context;
        private const int ViewTypeToday = 0;
        private const int ViewTypeFutureDay = 1;

        public ForecastAdapter(Activity context, List<ForecastInfo> objects) : base()
        {
            _forecast = objects;
            _context = context;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var data = _forecast[position];
            var viewType = GetItemViewType(position);
            var layoutId = viewType == ViewTypeToday ? Resource.Layout.ListItemTodayLayout : Resource.Layout.ListItemLayout;

            var view = convertView;
            ViewHolder viewHolder = null;

            if (view != null)
            {
                viewHolder = view.Tag as ViewHolder;
            }
            if (viewHolder == null)
            {
                // set the view based on the position
                view = _context.LayoutInflater.Inflate(layoutId, null);
                viewHolder = new ViewHolder(view);
                view.Tag = viewHolder;
            }

            // set the icon to a placeholder for right now
            viewHolder.IconView.SetImageResource(Resource.Drawable.Icon);

            // set the date view
            //var dateView = view.FindViewById<TextView>(Resource.Id.listItemDateTextView);
            viewHolder.DateView.Text = Utilities.GetFriendlyDate(data.Date);

            // set the description
            //var descriptionView = view.FindViewById<TextView>(Resource.Id.listItemForecastTextView);
            var descriptionText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.Details.Description);
            viewHolder.DescriptionView.Text = descriptionText;

            // set the high and low temperature
            var isMetric = Utilities.IsMetric(_context);
           // var highView = view.FindViewById<TextView>(Resource.Id.listItemHighTextView);
            viewHolder.HighTempView.Text = "High: " + Utilities.FormatTemperature(_context, Math.Round(data.Temperature.MaxTemperature), isMetric);

            //var lowView = view.FindViewById<TextView>(Resource.Id.listItemLowTextView);
            viewHolder.LowTempView.Text = "Low: " + Utilities.FormatTemperature(_context, Math.Round(data.Temperature.MinTemperature), isMetric);

            return view;
        }

        public void Clear()
        {
            _forecast.Clear();
        }

        public void AddAll(List<ForecastInfo> items)
        {
            _forecast.AddRange(items);
            NotifyDataSetChanged();
        }

        public override int Count => _forecast.Count;

        public override ForecastInfo this[int position] => _forecast[position];

        public override int ViewTypeCount => 2;

        public override int GetItemViewType(int position)
        {
            return (position == 0) ? ViewTypeToday : ViewTypeFutureDay;
        }
    }
}