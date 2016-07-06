
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
	public class BikeContainersAndBikeTheftsMenuActivity : MainActivity
	{
		// For the spinner
		private List<string> spinnerN = new List<string>();
		// To get the id through the value
		private Dictionary<int, string> Neighborhoods = new Dictionary<int, string>();

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.BikeContainersAndBikeTheftsMenu);
			this.registerReturnButton();

			// Query & getting the results.
			var db = Database.Load();
			string districtsQuery = "SELECT id,name from districts";
			var results = db.Query<District>(districtsQuery);
			// Adding the data to the list.
			foreach (District entry in results)
			{
				spinnerN.Add(entry.name);
				this.Neighborhoods[entry.id] = entry.name;
			}

			// Set up our adapter for the spinner
			var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, spinnerN);
			var spinner = FindViewById<Spinner>(Resource.Id.spinner);
			spinner.Adapter = adapter;

			Button resume = FindViewById<Button>(Resource.Id.continueButton);

			// Confirm button & it's event handler.
			resume.Click += delegate
			{
				// Our ID
				string currentSelection = (string) spinner.SelectedItem;
				int did = Neighborhoods.FirstOrDefault(x => x.Value == currentSelection).Key;

				// Our new activity
				var passedData = new Intent(this, typeof(BikeContainersAndBikeTheftsActivity));
				passedData.PutExtra("id", did.ToString());
				passedData.PutExtra("name", currentSelection);
				StartActivity(passedData);
			};

		}

	}
}

