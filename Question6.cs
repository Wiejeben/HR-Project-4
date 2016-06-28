using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Widget;
using Android.Gms.Location;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Util;
using Android.Locations;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System.Collections.Generic;

namespace Testapplicatie
{
	[Activity(Label = "@string/us_6")]
	public class Question6 : Activity, GoogleApiClient.IConnectionCallbacks,
		GoogleApiClient.IOnConnectionFailedListener, Android.Gms.Location.ILocationListener
	{
		GoogleApiClient apiClient;
		LocationRequest locRequest;
		Address address;
		string streetName;

		public async void GetLocation()
		{
			// Connect to the API
			apiClient.Connect();

			// We're connected to the API
			Log.Info("LocationClient", "Now connected to client");
			// If we really are connected
			if (apiClient.IsConnected)
			{
				// Wait for location updates
				await LocationServices.FusedLocationApi.RequestLocationUpdates(apiClient, locRequest, this);
			}
			else
			{
				// Wait for the connection to be made.
				Log.Info("LocationClient", "Please wait for Client to connect");
			}
		}

		////Interface methods
		public void OnConnected(Bundle bundle)
		{
			// Log message when we connect.
			Log.Info("LocationClient", "Now connected to client");
			// Call the getLocation function when we connect.
			GetLocation();
		}

		public void OnConnectionFailed(ConnectionResult bundle)
		{
			// Log message on connection fail
			Log.Info("LocationClient", "Connection failed, attempting to reach google play services");
		}

		public void OnConnectionSuspended(int i) { } // When the connection is suspended, we do nothing.

		public async void OnLocationChanged(Location location)
		{
			// Whenever the location changes, log message.
			Log.Debug("LocationClient", "Location updated");
			// Update the address.
			address = await LocationInformation.ReverseGeocodeCurrentLocation(this, location);
			// Get the streetname from the address we received.
			streetName = address.GetAddressLine(0).ToString();
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set layout view.
			SetContentView(Resource.Layout.Datepicker);

			// If GPS is installed for the geolocater..
			if (GooglePlayService.IsGooglePlayServicesInstalled(this))
			{
				// API client that will be used for the location
				apiClient = new GoogleApiClient.Builder(this, this, this).AddApi(LocationServices.API).Build();
				// generate a location request that we will pass into a call for location updates
				locRequest = new LocationRequest();
				// We get our location
				GetLocation();
			}
			else {
				// GPS is not installed, error message & return to home.
				Log.Error("OnCreate", "Google Play Services is not installed");
				Toast.MakeText(this, "Google Play Services is not installed", ToastLength.Long).Show();
				Finish();
			}

			// The used UI elements
			DatePicker datePicker = (DatePicker)FindViewById(Resource.Id.datePicker);
			Button confirmButton = FindViewById<Button>(Resource.Id.datePickerSelect);

			// Confirm button & it's event handler.
			confirmButton.Click += delegate
			{
				// The date
				int cDay = datePicker.DayOfMonth;
				int cMonth = datePicker.Month;
				int cYear = datePicker.Year;

				// Class that can save content for applications.
				ContentValues eventValues = new ContentValues();

				// We think the standard id for a calendar is 1 on a device.
				eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
				// Title for the agenda item.
				eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, "Fiets ophalen.");
				// Description for the agenda item.
				eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, "Anders word je fiets gejat!");
				// Location for the agenda item.
				eventValues.Put(CalendarContract.Events.InterfaceConsts.EventLocation, streetName);
				// Convert to milliseconds so we can define a start date
				eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, Helpers.convertToMilliseconds(cYear, cMonth, cDay));
				// Convert to milliseconds so we can define a end date.
				eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, Helpers.convertToMilliseconds(cYear, cMonth, cDay, 23, 59));

				// Define the timezones.
				eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "Europe/Berlin");
				eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "Europe/Berlin");

				// Insert the data into the agenda content.
				var uri = ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
				// Popup message
				Toast.MakeText(this, "Uw reminder is toevoegd!", ToastLength.Long).Show();

			};

			// Button & eventhandler.
			Button returnButton = FindViewById<Button>(Resource.Id.returnButton);
			returnButton.Click += delegate
			{
				// Swap to the right activity.
				StartActivity(typeof(MainActivity));
				// Close the current layout.
				Finish();
			};

		}
	}
}

