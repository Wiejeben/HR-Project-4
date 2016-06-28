using System;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using System.Collections.Generic;
using Android.Preferences;
using Android.Locations;
using Android.App;

namespace Testapplicatie
{
	public class General
	{
		// Hide keyboard
		public static void HideKeyboard(Activity parent)
		{
			View view = parent.CurrentFocus;
			if (view != null)
			{
				InputMethodManager imm = (InputMethodManager)parent.GetSystemService(Context.InputMethodService);
				imm.HideSoftInputFromWindow(view.WindowToken, 0);
			}
		}

		// Get a list of the saved locations
		public static List<string[]> GetSavedLocations(Activity parent)
		{
			List<string[]> locations = new List<string[]>();

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
						locations.Add(locationInformation);
					}
				}
			}

			return locations;
		}

		// Check if location is enabled
		public static bool LocationStatus(Activity activity)
		{
			LocationManager locationManager = (LocationManager)activity.GetSystemService(Context.LocationService);
			if (locationManager.IsProviderEnabled(LocationManager.GpsProvider))
			{
				return true;
			}
			else {
				return false;
			}
		}

        public string GetDatabasePath(Activity activity)
        {
            var FullDatabasePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/Database.sqlite";
            return FullDatabasePath;
        }
	}
}