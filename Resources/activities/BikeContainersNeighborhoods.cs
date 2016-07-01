using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;

using OxyPlot.Xamarin.Android;

namespace AndroidBicycleInfo
{
	[Activity(Label = "@string/us_3")]
	public class BikeContainersNeighborhoods : Activity
	{
		List<string> neighbourhoods = new List<string>();

		// Fake data
		List<int> thefts = new List<int>();

		Diagrams Diagrams = new Diagrams("Top 5 wijken met de meeste fietstrommels", "Hoeveelheid fietstrommels", "Wijken");

        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			// Set layout view.
			SetContentView(Resource.Layout.One_View);

			// Start database.
			Database.Boot(this);
			var db = Database.Load();
			// Query & getting the results.
			string TopContainersQuery = "SELECT d.name as name, COUNT(*) as drums FROM bikecontainers as b LEFT JOIN streets as s on s.id = b.street_id LEFT JOIN districts as d on d.id = s.district_id GROUP BY d.id ORDER BY drums DESC LIMIT 5";
			var results = db.Query<Street>(TopContainersQuery);
			// Adding the data to the list.
			foreach (Street entry in results)
			{
				neighbourhoods.Add(entry.name);
				thefts.Add(entry.drums);
			}

			// Reverse the values for the diagram.
			neighbourhoods.Reverse();
			thefts.Reverse();

			// Button & eventhandler.
			Button returnButton = FindViewById<Button>(Resource.Id.returnButton);
			returnButton.Click += delegate
			{
				// Swap to the right activity.
				StartActivity(typeof(MainActivity));
				// Close the current layout.
				Finish();
			};

			// Get the container for the model.
            PlotView view = FindViewById<PlotView>(Resource.Id.plotView);
			// Place the model in the container.
			view.Model = Diagrams.createBarModel(
				neighbourhoods,
				thefts
			);
        }
	}
}

