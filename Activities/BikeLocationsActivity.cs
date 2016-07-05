using System;
using Android.App;
using Android.OS;
using Android.Gms.Location;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Util;
using Android.Widget;
using Android.Locations;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System.Collections.Generic;


namespace AndroidBicycleInfo
{
	[Activity(Label = "@string/us_1")]
	public class BikeLocationsActivity : MainActivity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener, Android.Gms.Location.ILocationListener, IOnMapReadyCallback
	{
		GoogleApiClient apiClient;
		LocationRequest locRequest;
		Location location;
		GoogleMap map;

		// Lifecycle methods
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.BikeLocations);
			this.registerReturnButton();

			// Load spinner
			Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
			BikeLocationsElements.CreateSpinner(this, spinner);

			TextView locationName = FindViewById<TextView>(Resource.Id.LocationName);

			// Save locattion
			Button saveLocationButton = FindViewById<Button>(Resource.Id.saveLocation);
			saveLocationButton.Click += delegate
			{
				// Save location
				BikeLocationsElements.SaveLocation(this, location, locationName);

				// Hide keyboard
				General.HideKeyboard(this);

				// Reload spinner
				BikeLocationsElements.CreateSpinner(this, spinner);

				// Reload map
				InitMapFragment();

				// Display message
				Toast.MakeText(this, "Location has been saved", ToastLength.Long).Show();
				Map.CreateBikeMarkers(this, map);
			};

			// Show location on the map
			Button showLocationOnMap = FindViewById<Button>(Resource.Id.showLocationOnMap);
			showLocationOnMap.Click += delegate {
				Map.OpenMap(this, location);
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

		public async void OnLocationChanged(Location location)
		{
			// This method display changes in the user's location if they've been requested

			// You must implement this to implement the Android.Gms.Locations.ILocationListener Interface
			Log.Debug("LocationClient", "Location updated");

			this.location = location;

			Address address = await LocationInformation.ReverseGeocodeCurrentLocation(this, location);
			TextView locationName = FindViewById<TextView>(Resource.Id.LocationName);
			locationName.Text = address.GetAddressLine(0).ToString();

			InitMapFragment();
		}

		public void OnConnectionSuspended(int i) { }


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
			Map.CreateMarkerForCurrentLocation(this, location, address, map);

			// Add markers for the saved locations
			Map.CreateBikeMarkers(this, map);

			// Set map camera options
			map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(location.Latitude, location.Longitude), 18));
		}

		// If an bike is selected
		public void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;

			if (spinner.GetItemIdAtPosition(e.Position) != 0)
			{
				string toast = string.Format("De geselecteerde locatie is: {0}", spinner.GetItemAtPosition(e.Position));
				Toast.MakeText(this, toast, ToastLength.Long).Show();

				// Change camara to the selected location
				int counter = 0;
				List<string[]> savedLocations = General.GetSavedLocations(this);
				foreach (string[] savedLocation in savedLocations)
				{
					counter++;
					if (counter == spinner.GetItemIdAtPosition(e.Position))
					{
						map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(Double.Parse(savedLocation[1]), Double.Parse(savedLocation[2])), 18));
					}
				}
			}
		}
	}
}