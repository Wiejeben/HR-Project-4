using System;
using Android.Gms.Common;
using Android.Util;


namespace Testapplicatie
{
	public static class GooglePlayService
	{
		public static bool IsGooglePlayServicesInstalled(Android.Content.Context par1)
		{
			int queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(par1);
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

