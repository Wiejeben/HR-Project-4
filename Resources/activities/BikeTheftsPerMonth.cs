using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;

using OxyPlot.Xamarin.Android;

namespace AndroidBicycleInfo
{
	[Activity(Label = "@string/us_5")]
	public class BikeTheftsPerMonth : Activity
	{
		// Instance of the diagrams class.
		Diagrams Diagrams = new Diagrams("Gestolen fietsen per maand");
		// {month, value}
		Dictionary<int, int> lineValues = new Dictionary<int, int>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set layout view.
			SetContentView(Resource.Layout.One_View);


			// Start database.
			Database.Boot(this);
			var db = Database.Load();
			// Query & getting the results.
			string TopContainersQuery = "Select count(*) as thefts, strftime('%m', date, 'unixepoch') as month FROM bikethefts GROUP BY month";
			var results = db.Query<BikeTheft>(TopContainersQuery);
			// Adding the data to the list.
			foreach (BikeTheft entry in results)
			{
				lineValues.Add(entry.month, entry.thefts);
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

			// Find the container for our model
            PlotView view = FindViewById<PlotView>(Resource.Id.plotView);
			// Place our created model in the container w/ the values.
			view.Model = Diagrams.createLineModel(
				lineValues, 
				500
			);
        }
    }
}

