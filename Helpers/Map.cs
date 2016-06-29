﻿using System;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.Preferences;
using Android.Gms.Maps;
using Android.App;
using Android.Locations;
using Android.Util;
using Android.Widget;
using System.Collections.Generic;

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


		// Create a marker for the current location
		public static void CreateMarkersForBicyleDrums(Location currentLocation, GoogleMap map)
		{
			Dictionary<double, double> locations = new Dictionary<double, double>();
			locations.Add(51.924420, 4.477733); // Hofplein
			locations.Add(51.918024, 4.481692); // Blaak
			locations.Add(51.916491, 4.473635); // Eendrachtsplein

			foreach (var location in locations)
			{
				LatLng LatLngLocation = new LatLng(location.Key, location.Value);
				MarkerOptions markerOpt1 = new MarkerOptions();
				markerOpt1.SetPosition(LatLngLocation);
				markerOpt1.SetTitle("Something");
				map.AddMarker(markerOpt1);
			}
		}

		// Get the closest bicyledrum based on your current location
		public static void FocusOnClosestBicyleDrum(Location currentLocation, GoogleMap map)
		{
			LatLng currentLocationConverted = new LatLng(currentLocation.Latitude, currentLocation.Longitude);
			Dictionary<double, double> locations = new Dictionary<double, double>();
			double closestLocation = 999999;
			LatLng cameraPosition = new LatLng(currentLocation.Latitude, currentLocation.Longitude); ;
			locations.Add(51.924420, 4.477733); // Hofplein
			locations.Add(51.918024, 4.481692); // Blaak
			locations.Add(51.916491, 4.473635); // Eendrachtsplein

			foreach (var location in locations)
			{
				LatLng LatLngLocation = new LatLng(location.Key, location.Value);
				double result = Distance(currentLocationConverted, LatLngLocation);
				if (result < closestLocation)
				{
					closestLocation = result;
					cameraPosition = LatLngLocation;
				}
			}

			map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(cameraPosition, 18));
		}

		// Calculate distance from pos1 to pos in kilometers
		public static double Distance(LatLng pos1, LatLng pos2)
		{
			double R = 6371;

			double dLat = toRadian(pos2.Latitude - pos1.Latitude);
			double dLon = toRadian(pos2.Longitude - pos1.Longitude);

			double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
				Math.Cos(toRadian(pos1.Latitude)) * Math.Cos(toRadian(pos2.Latitude)) *
				Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
			double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
			double d = R * c;

			return d;
		}

		public static double toRadian(double val)
		{
			return (Math.PI / 180) * val;
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