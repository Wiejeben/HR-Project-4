﻿using System;
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
			spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(parent.spinner_ItemSelected);
			var adapter = new ArrayAdapter(parent, Android.Resource.Layout.SimpleSpinnerItem, GetSpinnerInfo(parent));
			adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spinner.Adapter = adapter;
		}

		public static List<string> GetSpinnerInfo(Question1 parent)
		{
			List<string> locations = new List<string>();
			locations.Add("Selecteer aan locatie");
		
			// Get the saved locations
			ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(parent);
			string Savedlocations = preferences.GetString("Locations", "");


			if (Savedlocations.Length > 0)
			{
				char delimiterChar1 = ';';
				char delimiterChar2 = '-';

				string[] locationsList = Savedlocations.Split(delimiterChar1);
				foreach (string locationList in locationsList)
				{
					if (locationList != "")
					{
						string[] locationInformation = locationList.Split(delimiterChar2);
						locations.Add(locationInformation[0]);
					}
				}
			}

			return locations;
		}
	
	}
}