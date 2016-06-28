using System;
using Android.Gms.Common;
using Android.Util;
using Android.Content;

namespace Testapplicatie
{
	public static class GooglePlayService
	{
		// Example call: if (GooglePlayService.IsGooglePlayServicesInstalled(this))
		public static bool IsGooglePlayServicesInstalled(Context parent)
		{
			int queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(parent);
			if (queryResult == ConnectionResult.Success)
			{
				Log.Info("MainActivity", "Google Play Services is installed on this device.");
				return true;
			}

			if (GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
			{
				string errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
				Log.Error("ManActivity", "There is a problem with Google Play Services on this device: {0} - {1}", queryResult, errorString);

				// Show error dialog to let user debug google play services
			}
			return false;
		}
	}
}