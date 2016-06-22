using System;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Android.App;
using Android.OS;
using Android.Gms.Location;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Util;
using Android.Widget;
using Android.Locations;
using System.Collections.Generic;

namespace Testapplicatie
{
	public static class LocationInformation
	{
		public static async Task<Address> ReverseGeocodeCurrentLocation(Android.Content.Context par1, Location location)
		{
			Geocoder geocoder = new Geocoder(par1);
			IList<Address> addressList = await geocoder.GetFromLocationAsync(location.Latitude, location.Longitude, 10);
			Address address = addressList.FirstOrDefault();
			return address;
		}

		public static string DisplayAddress(Address address)
		{
			if (address != null)
			{
				StringBuilder deviceAddress = new StringBuilder();
				for (int i = 0; i < address.MaxAddressLineIndex; i++)
				{
					deviceAddress.AppendLine(address.GetAddressLine(i));
				}
				// Remove the last comma from the end of the address.
				return deviceAddress.ToString();
			}
			else
			{
				return "Unable to determine the address. Try again in a few minutes.";
			}
		}
	}
}

