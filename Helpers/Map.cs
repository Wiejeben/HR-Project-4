using System;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.Preferences;
using Android.Gms.Maps;
using Android.App;
using Android.Locations;
using Android.Util;
using Android.Widget;

namespace Testapplicatie
{
	public class Map
	{
		// Create markers foreach saved bike location
		public static void CreateBikeMarkers(Activity parent, GoogleMap map)
		{
			ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(parent);
			string locations = preferences.GetString("Locations", "");
			if (locations.Length > 0)
			{
				char delimiterChar1 = ';';
				char delimiterChar2 = '-';

				string[] locationsList = locations.Split(delimiterChar1);
				foreach (string locationList in locationsList)
				{
					if (locationList != "")
					{
						string[] locationInformation = locationList.Split(delimiterChar2);
						// Add marker for current location	
						LatLng LatLngLocation = new LatLng(Convert.ToDouble(locationInformation[1]), Convert.ToDouble(locationInformation[2]));
						MarkerOptions markerOpt1 = new MarkerOptions();
						markerOpt1.SetPosition(LatLngLocation);
						markerOpt1.SetTitle(locationInformation[0]);
						map.AddMarker(markerOpt1);
					}
				}
			}
		}

		// Create a marker for the current location
		public static void CreateMarkerForCurrentLocation(Activity parent, Location location, Address address, GoogleMap map)
		{
			LatLng LatLngLocation = new LatLng(location.Latitude, location.Longitude);
			MarkerOptions markerOpt1 = new MarkerOptions();
			markerOpt1.SetPosition(LatLngLocation);
			markerOpt1.SetTitle(address.GetAddressLine(0).ToString());
			map.AddMarker(markerOpt1);
		}


		// Open map
		public static void OpenMap(Activity parent, Location location)
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
	}
}