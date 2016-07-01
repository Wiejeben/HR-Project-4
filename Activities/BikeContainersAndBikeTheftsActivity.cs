using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;

using OxyPlot.Xamarin.Android;

namespace AndroidBicycleInfo
{
	[Activity(Label = "@string/us_2")]
	public class BikeContainersAndBikeTheftsActivity : MainActivity
	{
		// Months
		List<string> months = new List<string>(
			new string[] {
				"December",
				"November",
				"Oktober",
				"September",
				"Augustus",
				"Juli",
				"Juni",
				"Mei",
				"April",
				"Maart",
				"Februari",
				"Januari"
		});

        // Fake data
        List<int> containers = new List<int>();

        // Fake data
        List<int> thefts = new List<int>();

        // Diagram class instance
        Diagram Diagrams = new Diagram("Fietstrommels & fietsdiefstallen per maand");

        protected override void OnCreate(Bundle savedInstanceState)
		{
			// Create the instance on the base
			base.OnCreate(savedInstanceState);
			// Set view.
			SetContentView(Resource.Layout.One_View);

			// Our container for the model
            PlotView view = FindViewById<PlotView>(Resource.Id.plotView);

            var db = Database.Load();
            // Query & getting the results.
            string TopContainersQuery = "Select  d.name as name, d.id as did, COUNT(b.id) as drums, count(bt.id) as thefts ,  strftime('%m', date, 'unixepoch') as month FROM bikethefts as bt LEFT JOIN streets as s on s.id = bt.street_id LEFT JOIN districts as d on d.id = s.district_id LEFT JOIN bikecontainers as b on b.street_id = s.id WHERE did = 16 GROUP BY month";
            var results = db.Query<BikeTheft>(TopContainersQuery);
            // Adding the data to the list.
            foreach (BikeTheft entry in results)
            {
                containers.Add(entry.container);
                thefts.Add(entry.thefts);
            }

            // Create the model (diagrams.f) and place it in the view (view.model)
            view.Model = Diagrams.CreateTwoBarModel(
				months,
				"Geinstalleerde fietstrommels",
                containers,
				"Fietsdiefstallen",
                thefts
            );
        }
	}
}

