using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Object = Java.Lang.Object;

namespace Spring_Time
{
    public class SettingsFragment : PreferenceFragment, Preference.IOnPreferenceChangeListener
    {
        public SettingsFragment()
        {
            
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AddPreferencesFromResource(Resource.Xml.GeneralPreference);

            // bind all the preferences
            BindPreferencesSummaryToValue(FindPreference(GetString(Resource.String.LocationPrefKey)));
            BindPreferencesSummaryToValue(FindPreference(GetString(Resource.String.TempPrefKey)));
        }

        private void BindPreferencesSummaryToValue(Preference preference)
        {
            preference.OnPreferenceChangeListener = this;

            OnPreferenceChange(preference,
                PreferenceManager.GetDefaultSharedPreferences(preference.Context).GetString(preference.Key, ""));
        }

        public bool OnPreferenceChange(Preference preference, Object newValue)
        {
            var stringValue = newValue.ToString();
            var listPreference = preference as ListPreference;

            // for list preferences
            if (listPreference != null)
            {
                int prefIndex = listPreference.FindIndexOfValue(stringValue);
                if (prefIndex >= 0)
                {
                    preference.Summary = listPreference.GetEntries()[prefIndex];
                }
            }
            // any other preference just set the summary
            else
            {
                preference.Summary = stringValue;
            }

            return true;
        }
    }
}