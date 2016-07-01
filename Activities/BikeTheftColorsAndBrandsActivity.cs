using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using OxyPlot.Xamarin.Android;

namespace AndroidBicycleInfo
{
	[Activity(Label = "@string/us_4")]
	public class BikeTheftColorsAndBrandsActivity : Activity
	{
		private Diagram TheftByBrand = new Diagram("Gestolen fietsen op basis van merk");
		private Diagram TheftByColor = new Diagram("Gestolen fietsen op basis van kleur");

        Dictionary<string, int> BrandData = new Dictionary<string, int>();
        Dictionary<string, int> ColorData = new Dictionary<string, int>();

		protected override void OnCreate(Bundle bundle)
		{
            base.OnCreate(bundle);
			SetContentView(Resource.Layout.Two_Views);

            // Get data
            var database = Database.Load();
            string TopFiveTheftsPerBrand = "SELECT br.name, COUNT(bt.brand_id) AS total_stolen FROM `bikethefts` AS bt LEFT JOIN `brands` AS br WHERE bt.brand_id = br.id GROUP BY bt.brand_id ORDER BY total_stolen DESC LIMIT 5;";
            string TopFiveTheftsPerColor = "SELECT c.name, COUNT(bt.color_id) AS total_stolen FROM `bikethefts` AS bt LEFT JOIN `colors` AS c WHERE bt.color_id = c.id GROUP BY bt.color_id ORDER BY total_stolen DESC LIMIT 5;";
			database.Query<Brand>(TopFiveTheftsPerBrand).ForEach(value => this.BrandData.Add(value.name, value.total_stolen));
			database.Query<Color>(TopFiveTheftsPerColor).ForEach(value => this.ColorData.Add(value.name, value.total_stolen));

			// Create pie charts
			PlotView view = FindViewById<PlotView>(Resource.Id.plotView);
			PlotView viewTwo = FindViewById<PlotView>(Resource.Id.plotView2);

			// Apply PlotModel
			view.Model = TheftByBrand.CreatePieModel(this.BrandData);
			viewTwo.Model = TheftByColor.CreatePieModel(this.ColorData);

			// Return
			Button returnButton = FindViewById<Button>(Resource.Id.returnButton);
			returnButton.Click += delegate
			{
				StartActivity(typeof(MainActivity));
				Finish();
			};
		}
	}
}
