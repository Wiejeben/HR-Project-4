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



namespace Testapplicatie
{
	[Activity(Label = "@string/v1")]
	public class Question1 : Activity, GoogleApiClient.IConnectionCallbacks,
		GoogleApiClient.IOnConnectionFailedListener, Android.Gms.Location.ILocationListener, IOnMapReadyCallback
	{
		GoogleApiClient apiClient;
		LocationRequest locRequest;
		TextView latitude;
		TextView longitude;
		TextView provider;
		TextView locationName;
		Location location;
		GoogleMap _map;


		////Lifecycle methods
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			Log.Debug("OnCreate", "OnCreate called, initializing views...");

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Question_One);

			// UI to print last location
			latitude = FindViewById<TextView>(Resource.Id.latitude);
			longitude = FindViewById<TextView>(Resource.Id.longitude);
			provider = FindViewById<TextView>(Resource.Id.provider);
			locationName = FindViewById<TextView>(Resource.Id.locationName);

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


		public async void GetLocation()
		{
			apiClient.Connect();

			// This method is called when we connect to the LocationClient. We can start location updated directly form
			// here if desired, or we can do it in a lifecycle method, as shown above 

			// You must implement this to implement the IGooglePlayServicesClientConnectionCallbacks Interface
			Log.Info("LocationClient", "Now connected to client");
			if (apiClient.IsConnected)
			{
				// Setting location priority to PRIORITY_HIGH_ACCURACY (100)
				locRequest.SetPriority(100);

				// Setting interval between updates, in milliseconds
				// NOTE: the default FastestInterval is 1 minute. If you want to receive location updates more than 
				// once a minute, you _must_ also change the FastestInterval to be less than or equal to your Interval
				locRequest.SetFastestInterval(500);
				locRequest.SetInterval(1000);

				Log.Debug("LocationRequest", "Request priority set to status code {0}, interval set to {1} ms",
					locRequest.Priority.ToString(), locRequest.Interval.ToString());

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

		public async void OnLocationChanged(Location location)
		{
			// This method display changes in the user's location if they've been requested

			// You must implement this to implement the Android.Gms.Locations.ILocationListener Interface
			Log.Debug("LocationClient", "Location updated");

			latitude.Text = "Latitude: " + location.Latitude.ToString();
			longitude.Text = "Longitude: " + location.Longitude.ToString();
			provider.Text = "Provider: " + location.Provider.ToString();

			this.location = location;
			Address address = await LocationInformation.ReverseGeocodeCurrentLocation(this, location);
			locationName.Text = LocationInformation.DisplayAddress(address);

			InitMapFragment();
		}

		public void OnConnectionSuspended(int i) { }


		private void InitMapFragment()
		{
			if (_map == null)
			{
				FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);

				LatLng LatLngLocation = new LatLng(location.Latitude, location.Longitude);
				CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
				builder.Target(LatLngLocation);
				builder.Zoom(18);

				CameraPosition cameraPosition = builder.Build();
				GoogleMapOptions mapOptions = new GoogleMapOptions()
					.InvokeMapType(GoogleMap.MapTypeSatellite)
					.InvokeZoomControlsEnabled(false)
					.InvokeCompassEnabled(true)
					.InvokeCamera(cameraPosition);

				FragmentTransaction fragTx = FragmentManager.BeginTransaction();
				MapFragment _mapFragment = MapFragment.NewInstance(mapOptions);
				fragTx.Add(Resource.Id.map, _mapFragment, "map");
				fragTx.Commit();
				_mapFragment.GetMapAsync(this);
			}
		}

		public async void OnMapReady(GoogleMap googleMap)
		{
			_map = googleMap;
			Address address = await LocationInformation.ReverseGeocodeCurrentLocation(this, location);

			LatLng LatLngLocation = new LatLng(location.Latitude, location.Longitude);
			MarkerOptions markerOpt1 = new MarkerOptions();
			markerOpt1.SetPosition(LatLngLocation);
			markerOpt1.SetTitle(address.GetAddressLine(0).ToString());
			_map.AddMarker(markerOpt1);
		}
	}
}