using System.Collections.Generic;
using Android.App;
using Android.OS;
using OxyPlot.Xamarin.Android;

namespace AndroidBicycleInfo
{
	[Activity(Label = "@string/us_3")]
	public class BikeContainerNeighborhoodsActivity : MainActivity
	{

		Dictionary<string, int> Data = new Dictionary<string, int>();
		Diagram Diagram = new Diagram("Top 5 wijken met de meeste fietstrommels", "Wijken", "Hoeveelheid fietstrommels");

        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.One_View);
			this.registerReturnButton();

			// Get database and make it graph compatible
			var db = Database.Load();
			string TopContainersQuery = "SELECT d.name as name, COUNT(*) as drums FROM bikecontainers as b LEFT JOIN streets as s on s.id = b.street_id LEFT JOIN districts as d on d.id = s.district_id GROUP BY d.id ORDER BY drums DESC LIMIT 5";
			List<Street> results = db.Query<Street>(TopContainersQuery);
			results.Reverse();
			results.ForEach(value => this.Data.Add(value.name, value.drums));

			// Generate table and apply to view
            PlotView view = FindViewById<PlotView>(Resource.Id.plotView);
			view.Model = Diagram.CreateBarModel(this.Data);
        }
	}
}

