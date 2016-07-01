using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using OxyPlot.Xamarin.Android;

namespace AndroidBicycleInfo
{
	[Activity(Label = "@string/us_5")]
	public class BikeTheftsPerMonthActivity : MainActivity
	{

		Diagram Diagram = new Diagram("Gestolen fietsen per maand");
		Dictionary<int, int> Data = new Dictionary<int, int>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			SetContentView(Resource.Layout.One_View);
			this.registerReturnButton();

			// Query & getting the results.
			var db = Database.Load();
			string TopContainersQuery = "Select count(*) as thefts, strftime('%m', date, 'unixepoch') as month FROM bikethefts GROUP BY month";
			var results = db.Query<BikeTheft>(TopContainersQuery);
			results.ForEach(value => this.Data.Add(value.month, value.thefts));

            PlotView view = FindViewById<PlotView>(Resource.Id.plotView);
			view.Model = this.Diagram.CreateLineModel(
				this.Data, 
				500,
				"Maand",
				"Diefstellen"
			);
        }
    }
}

