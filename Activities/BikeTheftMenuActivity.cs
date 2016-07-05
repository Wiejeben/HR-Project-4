
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AndroidBicycleInfo
{
	[Activity(Label = "@string/us_2_menu")]
	public class BikeTheftMenuActivity : MainActivity
	{
		// For the spinner
		private List<int> spinnerN = new List<int>();

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.BikeContainersAndBikeTheftsMenu);
			this.registerReturnButton();

			// Query & getting the results.
			var db = Database.Load();
			string yearQuery = "Select strftime('%Y', date, 'unixepoch') as year FROM bikethefts GROUP BY year";
			var results = db.Query<BikeTheft>(yearQuery);
			// Adding the data to the list.
			foreach (BikeTheft entry in results)
			{
				if (entry.year != 1992 && entry.year != 2020)
				{
					spinnerN.Add(entry.year);
				}
			}

			// Set up our adapter for the spinner
			var adapter = new ArrayAdapter<int>(this, Android.Resource.Layout.SimpleSpinnerItem, spinnerN);
			var spinner = FindViewById<Spinner>(Resource.Id.spinner);
			spinner.Adapter = adapter;

			Button resume = FindViewById<Button>(Resource.Id.continueButton);

			// Confirm button & it's event handler.
			resume.Click += delegate
			{
				// Our ID
				string currentSelection = (string) spinner.SelectedItem;

				// Our new activity
				var passedData = new Intent(this, typeof(BikeTheftsPerMonthActivity));
				passedData.PutExtra("year", currentSelection);
				StartActivity(passedData);
			};

		}

	}
}

