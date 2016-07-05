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


        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			string did = Intent.GetStringExtra("id");
			string dname = Intent.GetStringExtra("name");

			SetContentView(Resource.Layout.BikeContainersAndBikeThefts);
			this.registerReturnButton();

			// Our container for the model
			PlotView view = FindViewById<PlotView>(Resource.Id.plotView);

			// Query & getting the results.
			var db = Database.Load();
			string TopContainersQuery = "SELECT d.name as name, d.id AS did, COUNT(b.id) AS drums, COUNT(bt.id) AS thefts, strftime('%m', bt.date, 'unixepoch') AS month FROM bikethefts AS bt LEFT JOIN streets AS s ON s.id = bt.street_id LEFT JOIN districts AS d ON d.id = s.district_id LEFT JOIN bikecontainers AS b ON b.street_id = s.id WHERE did = ";
			TopContainersQuery += did; 
			TopContainersQuery += " GROUP BY month";

            var results = db.Query<BikeTheft>(TopContainersQuery);
            // Adding the data to the list.
            foreach (BikeTheft entry in results)
            {
				this.Containers.Add(entry.container);
				this.Thefts.Add(entry.thefts);
            }

			Diagram Diagrams = new Diagram(dname);

            // Create the model (diagrams.f) and place it in the view (view.model)
            view.Model = Diagrams.CreateTwoBarModel(
				this.Months,
				"Geinstalleerde fietstrommels",
				this.Containers,
				"Fietsdiefstallen",
				this.Thefts
            );

			Button sreturnButton = FindViewById<Button>(Resource.Id.sreturnButton);
			// Return to the menu button & it's event handler.
			sreturnButton.Click += delegate
			{
				StartActivity(typeof(BikeContainersAndBikeTheftsMenuActivity));
				Finish();
			};
        }
	}
}

