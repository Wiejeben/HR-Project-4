using System;
using Android.Util;
using Android.Widget;
using Android.Locations;
using Android.Content;
using Android.Preferences;
using System.Collections.Generic;

namespace Testapplicatie
{
	public class Question1Elements
	{
		// Open map
		public static void OpenMap(Question1 parent, Location location)
		{
			if (location == null)
			{
				Log.Error("OnShowLocationOnMapButtonClick", "No location has been found to display on the map");
				Toast.MakeText(parent, "No location has been found.", ToastLength.Long).Show();
			}
			else {
				var geoUri = Android.Net.Uri.Parse("geo:" + location.Latitude + "," + location.Longitude);
				var mapIntent = new Intent(Intent.ActionView, geoUri);
				parent.StartActivity(mapIntent);
			}
		}

		// Save location
		public static void SaveLocation(Question1 parent, Location location, TextView locationName)
		{
			// Add the new location to the saved locations
			ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(parent);
			string locations = preferences.GetString("Locations", "");

			locations += locationName.Text + "-" + location.Latitude + "-" + location.Longitude + ";";

			ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(parent);
			ISharedPreferencesEditor editor = prefs.Edit();
			editor.PutString("Locations", locations);
			editor.Apply();

			Log.Debug("OnLocationSave", locations);
		}

		// Spiner
		public static void CreateSpinner(Question1 parent, Spinner spinner)
		{
			var items = new List<string>() { "one", "two", "three" };
			spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(parent.spinner_ItemSelected);
			var adapter = new ArrayAdapter(parent, Android.Resource.Layout.SimpleSpinnerItem, items);
			adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spinner.Adapter = adapter;
		}

	
	}
}