using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;

using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Testapplicatie
{
	[Activity(Label = "@string/us_6")]
	public class Question6 : Activity
	{
		public int cDay 	= 	DateTime.Now.Day;
		public int cMonth 	= 	(DateTime.Now.Month - 1);
		public int cYear 	= 	DateTime.Now.Year;
		public int cHour 	= 	DateTime.Now.Hour;
		public int cMin 	= 	DateTime.Now.Minute;

		Router router = new Router();

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set layout view.
			// SetContentView(Resource.Layout.Question_One);

			// Class that can save content for applications.
			ContentValues eventValues = new ContentValues();

			// We think the standard id for a calendar is 1 on a device.
			eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
			// Title for the agenda item.
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, "Fiets ophalen.");
			// Description for the agenda item.
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, "Anders word je fiets gejat!");
			// Location for the agenda item.
			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventLocation, "Somewhere..");
			// Convert to milliseconds so we can define a start date
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, Helpers.convertToMilliseconds(cYear, cMonth, cDay, cHour, cMin));
			// Convert to milliseconds so we can define a end date.
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, Helpers.convertToMilliseconds(cYear, cMonth, cDay+1, cHour, cMin));

			// Define the timezones.
			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "Europe/Berlin");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "Europe/Berlin");

			// Insert the data into the agenda content.
			var uri = ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);

			// Let's return to the main activity.
			StartActivity(typeof(MainActivity));

			// Button & eventhandler.
			/* Button returnButton = FindViewById<Button>(Resource.Id.returnButton);
			returnButton.Click += delegate
			{
				// Swap to the right activity.
				StartActivity(typeof(MainActivity));
				// Close the current layout.
				Finish();
			};
			*/
		}
	}
}

