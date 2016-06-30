using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;

using OxyPlot.Xamarin.Android;

namespace Testapplicatie
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

			// Start database
			Database.Boot(this);
			// Connect database
			var db = Database.Load();
			// Query
			string TopContainersQuery = "SELECT b.id, s.name, COUNT(*) as drums FROM bikecontainers as b LEFT JOIN streets as s ON s.id = b.street_id GROUP BY street_id ORDER BY drums DESC LIMIT 5";
			// Results
			var results = db.Query<Street>(TopContainersQuery);

			foreach (Street entry in results)
			{
				Toast.MakeText(this, entry.drums, ToastLength.Short).Show();
			}

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

