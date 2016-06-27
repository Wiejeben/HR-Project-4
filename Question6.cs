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
			apiClient.Connect();

			// This method is called when we connect to the LocationClient. We can start location updated directly form
			// here if desired, or we can do it in a lifecycle method, as shown above 

			// You must implement this to implement the IGooglePlayServicesClientConnectionCallbacks Interface
			Log.Info("LocationClient", "Now connected to client");
			if (apiClient.IsConnected)
			{
				// pass in a location request and LocationListener
				await LocationServices.FusedLocationApi.RequestLocationUpdates(apiClient, locRequest, this);
				// In OnLocationChanged (below), we will make calls to update the UI
				// with the new location data
			}
			else
			{
				Log.Info("LocationClient", "Please wait for Client to connect");
			}
		}

		////Interface methods
		public void OnConnected(Bundle bundle)
		{
			// This method is called when we connect to the LocationClient. We can start location updated directly form
			// here if desired, or we can do it in a lifecycle method, as shown above 

			// You must implement this to implement the IGooglePlayServicesClientConnectionCallbacks Interface
			Log.Info("LocationClient", "Now connected to client");
			GetLocation();
		}

		public void OnConnectionFailed(ConnectionResult bundle)
		{
			// This method is used to handle connection issues with the Google Play Services Client (LocationClient). 
			// You can check if the connection has a resolution (bundle.HasResolution) and attempt to resolve it

			// You must implement this to implement the IGooglePlayServicesClientOnConnectionFailedListener Interface
			Log.Info("LocationClient", "Connection failed, attempting to reach google play services");
		}

		public void OnConnectionSuspended(int i) { }

		public async void OnLocationChanged(Location location)
		{
			// This method display changes in the user's location if they've been requested

			// You must implement this to implement the Android.Gms.Locations.ILocationListener Interface
			Log.Debug("LocationClient", "Location updated");

			address = await LocationInformation.ReverseGeocodeCurrentLocation(this, location);
			streetName = address.GetAddressLine(0).ToString();
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set layout view.
			SetContentView(Resource.Layout.Datepicker);

			if (GooglePlayService.IsGooglePlayServicesInstalled(this))
			{
				apiClient = new GoogleApiClient.Builder(this, this, this).AddApi(LocationServices.API).Build();
				// generate a location request that we will pass into a call for location updates
				locRequest = new LocationRequest();
				GetLocation();
			}
			else {
				Log.Error("OnCreate", "Google Play Services is not installed");
				Toast.MakeText(this, "Google Play Services is not installed", ToastLength.Long).Show();
				Finish();
			}

			DatePicker datePicker = (DatePicker)FindViewById(Resource.Id.datePicker);
			Button confirmButton = FindViewById<Button>(Resource.Id.datePickerSelect);

			confirmButton.Click += delegate
			{
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

