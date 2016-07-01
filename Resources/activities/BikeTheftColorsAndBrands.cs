using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

namespace AndroidBicycleInfo
{
	[Activity(Label = "@string/us_4")]
	public class BikeTheftColorsAndBrands : Activity
	{
		private Diagram TheftByBrand = new Diagram("Gestolen fietsen op basis van merk");
		private Diagram TheftByColor = new Diagram("Gestolen fietsen op basis van kleur");

        Dictionary<string, int> BrandData = new Dictionary<string, int>();
        Dictionary<string, int> ColorData = new Dictionary<string, int>();

		protected override void OnCreate(Bundle bundle)
		{
            base.OnCreate(bundle);

			// Set layout view.
			SetContentView(Resource.Layout.Two_Views);

            Database.Boot(this);
            var database = Database.Load();
            string TopFiveTheftsPerBrand = "SELECT br.name, COUNT(bt.brand_id) AS total_stolen FROM `bikethefts` AS bt LEFT JOIN `brands` AS br WHERE bt.brand_id = br.id GROUP BY bt.brand_id ORDER BY total_stolen DESC LIMIT 5;";
            string TopFiveTheftsPerColor = "SELECT c.name, COUNT(bt.color_id) AS total_stolen FROM `bikethefts` AS bt LEFT JOIN `colors` AS c WHERE bt.color_id = c.id GROUP BY bt.color_id ORDER BY total_stolen DESC LIMIT 5;";
            var BrandResult = database.Query<Brand>(TopFiveTheftsPerBrand);
            var ColorResult = database.Query<Color>(TopFiveTheftsPerColor);

            foreach (Brand brand in BrandResult)
            {
                BrandData.Add(brand.name, brand.total_stolen);
            }

            foreach (Color color in ColorResult)
            {
                ColorData.Add(color.name, color.total_stolen);
            }

			// Button & eventhandler.
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Two_Views);

			// Return
			Button returnButton = FindViewById<Button>(Resource.Id.returnButton);
			returnButton.Click += delegate
			{
				StartActivity(typeof(MainActivity));
				Finish();
			};

			// Create the first pie chart.
			PlotView view = FindViewById<PlotView>(Resource.Id.plotView);
			view.Model = TheftByBrand.CreatePieModel(BrandData);

			// Create the second pie chart.
			PlotView viewTwo = FindViewById<PlotView>(Resource.Id.plotView2);
			viewTwo.Model = TheftByColor.CreatePieModel(ColorData);
		}
	}
}
