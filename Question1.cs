using System;
using Android.App;
using Android.OS;
using Android.Gms.Location;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Util;
using Android.Widget;
using Android.Locations;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Preferences;
using Android.Views.InputMethods;
using Android.Views;

namespace Testapplicatie
{
	[Activity(Label = "@string/v1")]
	public class Question1 : Activity, GoogleApiClient.IConnectionCallbacks,
		GoogleApiClient.IOnConnectionFailedListener, Android.Gms.Location.ILocationListener, IOnMapReadyCallback
	{
		GoogleApiClient apiClient;
		LocationRequest locRequest;
		Location location;
		GoogleMap map;

		////Lifecycle methods
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			Log.Debug("OnCreate", "OnCreate called, initializing views...");

			// Set our view from the "main" layout resource
			//SetContentView(Resource.Layout.One_View);
			SetContentView(Resource.Layout.Base);

			// Save locattion
			Button saveLocationButton = FindViewById<Button>(Resource.Id.saveLocation);
			saveLocationButton.Click += delegate
			{
				// Add the new location to the saved locations
				ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(this);
				string locations = preferences.GetString("Locations", "");
				TextView locationName = FindViewById<TextView>(Resource.Id.LocationName);
				locations += locationName.Text + "-" + location.Latitude + "-" + location.Longitude + ";";

				ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
				ISharedPreferencesEditor editor = prefs.Edit();
				editor.PutString("Locations", locations);
				editor.Apply();

				// Hide keyboard
				View view = this.CurrentFocus;
				if (view != null)
				{
					InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
					imm.HideSoftInputFromWindow(view.WindowToken, 0);
				}

				// Display message
				Toast.MakeText(this, "Location has been saved", ToastLength.Long).Show();
				CreateMarkers();

				Log.Debug("OnLocationSave", locations);
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

			// Show location on the map
			Button showLocationOnMap = FindViewById<Button>(Resource.Id.showLocationOnMap);
			showLocationOnMap.Click += delegate
			{
				if (location == null)
				{
					Log.Error("OnShowLocationOnMapButtonClick", "No location has been found to display on the map");
					Toast.MakeText(this, "No location has been found.", ToastLength.Long).Show();
				}
				else {
					var geoUri = Android.Net.Uri.Parse("geo:" + location.Latitude + "," + location.Longitude);
					var mapIntent = new Intent(Intent.ActionView, geoUri);
					StartActivity(mapIntent);
				}
			};

			if (GooglePlayService.IsGooglePlayServicesInstalled(this))
			{
				apiClient = new GoogleApiClient.Builder(this, this, this).AddApi(LocationServices.API).Build();

				// generate a location request that we will pass into a call for location updates
				locRequest = new LocationRequest();
			}
			else {
				Log.Error("OnCreate", "Google Play Services is not installed");
				Toast.MakeText(this, "Google Play Services is not installed", ToastLength.Long).Show();
				Finish();
			}
		}


		protected override void OnResume()
		{
			base.OnResume();
			Log.Debug("OnResume", "OnResume called, connecting to client...");

			GetLocation();
		}

		protected override async void OnPause()
		{
			base.OnPause();
			Log.Debug("OnPause", "OnPause called, stopping location updates");

			if (apiClient.IsConnected)
			{
				// stop location updates, passing in the LocationListener
				await LocationServices.FusedLocationApi.RemoveLocationUpdates(apiClient, this);

				apiClient.Disconnect();
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

		public void OnDisconnected()
		{
			// This method is called when we disconnect from the LocationClient.

			// You must implement this to implement the IGooglePlayServicesClientConnectionCallbacks Interface
			Log.Info("LocationClient", "Now disconnected from client");
		}

		public void OnConnectionFailed(ConnectionResult bundle)
		{
			// This method is used to handle connection issues with the Google Play Services Client (LocationClient). 
			// You can check if the connection has a resolution (bundle.HasResolution) and attempt to resolve it

			// You must implement this to implement the IGooglePlayServicesClientOnConnectionFailedListener Interface
			Log.Info("LocationClient", "Connection failed, attempting to reach google play services");
		}

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

		public void OnLocationChanged(Location location)
		{
			// This method display changes in the user's location if they've been requested

			// You must implement this to implement the Android.Gms.Locations.ILocationListener Interface
			Log.Debug("LocationClient", "Location updated");

			this.location = location;
			InitMapFragment();
		}

		public void OnConnectionSuspended(int i) { }

		public void CreateMarkers()
		{
			ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(this);
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


		private void InitMapFragment()
		{
			if (map == null)
			{
				FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
			}
		}


		public async void OnMapReady(GoogleMap googleMap)
		{
			map = googleMap;
			// Get adress info
			Address address = await LocationInformation.ReverseGeocodeCurrentLocation(this, location);

			// Add marker for current location
			LatLng LatLngLocation = new LatLng(location.Latitude, location.Longitude);
			MarkerOptions markerOpt1 = new MarkerOptions();
			markerOpt1.SetPosition(LatLngLocation);
			markerOpt1.SetTitle(address.GetAddressLine(0).ToString());
			map.AddMarker(markerOpt1);

			// Add markers for the saved locations
			CreateMarkers();

			// Set map options
			googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(location.Latitude, location.Longitude), 18));
		}
	}
}