using System;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.Preferences;
using Android.Gms.Maps;

namespace Testapplicatie
{
	public class Map
	{
		public static void CreateMarkers(Question1 parent, GoogleMap map)
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
	}
}