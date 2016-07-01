using System.Collections.Generic;
using Android.App;
using Android.OS;
using OxyPlot.Xamarin.Android;

namespace AndroidBicycleInfo
{
	[Activity(Label = "@string/us_2")]
	public class BikeContainersAndBikeTheftsActivity : MainActivity
	{
		private List<string> Months = new List<string>(
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

        private List<int> Containers = new List<int>();
        private List<int> Thefts = new List<int>();

        private Diagram Diagrams = new Diagram("Fietstrommels & fietsdiefstallen per maand");

        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.One_View);
			this.registerReturnButton();

			// Our container for the model
            PlotView view = FindViewById<PlotView>(Resource.Id.plotView);

			// Query & getting the results.
			var db = Database.Load();
            string TopContainersQuery = "SELECT d.name as name, d.id AS did, COUNT(b.id) AS drums, COUNT(bt.id) AS thefts, strftime('%m', date, 'unixepoch') AS month FROM bikethefts AS bt LEFT JOIN streets AS s ON s.id = bt.street_id LEFT JOIN districts AS d ON d.id = s.district_id LEFT JOIN bikecontainers AS b ON b.street_id = s.id WHERE did = 16 GROUP BY month";
            var results = db.Query<BikeTheft>(TopContainersQuery);
            // Adding the data to the list.
            foreach (BikeTheft entry in results)
            {
				this.Containers.Add(entry.container);
				this.Thefts.Add(entry.thefts);
            }

            // Create the model (diagrams.f) and place it in the view (view.model)
            view.Model = Diagrams.CreateTwoBarModel(
				this.Months,
				"Geinstalleerde fietstrommels",
				this.Containers,
				"Fietsdiefstallen",
				this.Thefts
            );
        }
	}
}

