using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using OxyPlot.Xamarin.Android;


namespace AndroidBicycleInfo
{
	[Activity(Label = "@string/us_4")]
	public class BikeTheftColorsAndBrandsActivity : MainActivity
	{
		private Diagram TheftByBrand = new Diagram("Top 5 gestolen fietsen op basis van merk");
		private Diagram TheftByColor = new Diagram("Top 5 gestolen fietsen op basis van kleur");

        Dictionary<string, int> BrandData = new Dictionary<string, int>();
        Dictionary<string, int> ColorData = new Dictionary<string, int>();

		protected override void OnCreate(Bundle bundle)
		{
            base.OnCreate(bundle);
			SetContentView(Resource.Layout.Two_Views);
			this.registerReturnButton();

            // Get data
            var database = Database.Load();
            string TopFiveTheftsPerBrand = "SELECT br.name, COUNT(bt.brand_id) AS total_stolen FROM `bikethefts` AS bt LEFT JOIN `brands` AS br WHERE bt.brand_id = br.id GROUP BY bt.brand_id ORDER BY total_stolen DESC;";
            string TopFiveTheftsPerColor = "SELECT c.name, COUNT(bt.color_id) AS total_stolen FROM `bikethefts` AS bt LEFT JOIN `colors` AS c WHERE bt.color_id = c.id GROUP BY bt.color_id ORDER BY total_stolen DESC;";

			List<Brand> brands = database.Query<Brand>(TopFiveTheftsPerBrand);
			List<Brand> newBrands = new List<Brand>();

			foreach (var item in brands)
			{
				if (newBrands.Count > 5)
				{
					Brand lastBrand = newBrands.Last();

					lastBrand.name = "Overige";
					lastBrand.total_stolen += item.total_stolen;
				}
				else
				{
					newBrands.Add(item);
				}
			}

			newBrands.ForEach(value => this.BrandData.Add(value.name, value.total_stolen));
			database.Query<Color>(TopFiveTheftsPerColor).ForEach(value => this.ColorData.Add(value.name, value.total_stolen));

			// Create pie charts
			PlotView view = FindViewById<PlotView>(Resource.Id.plotView);
			PlotView viewTwo = FindViewById<PlotView>(Resource.Id.plotView2);

			// Apply PlotModel
			view.Model = TheftByBrand.CreatePieModel(this.BrandData);
			viewTwo.Model = TheftByColor.CreatePieModel(this.ColorData, true);
		}
	}
}
