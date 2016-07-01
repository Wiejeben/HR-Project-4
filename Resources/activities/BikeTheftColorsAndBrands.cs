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
		// Two instances because we need one for every model and the plot model gets created in the constructor.
		// It's impossible to re-use an old plot model.
		Diagrams Diagrams 		= new Diagrams("Gestolen fietsen op basis van merk");
		Diagrams DiagramsSecond = new Diagrams("Gestolen fietsen op basis van kleur");

		// fake data
		// {name, value}
		/*Dictionary<string, int> chartValues = new Dictionary<string, int>()
		{
			{"Een"  , 50},
			{"Twee" , 75},
			{"Drie" , 20},
			{"Vier" , 12},
			{"Vijf" , 30},
		};*/

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
			Button returnButton = FindViewById<Button>(Resource.Id.returnButton);
			returnButton.Click += delegate
			{
				// Swap to the right activity.
				StartActivity(typeof(MainActivity));
				// Close the current layout.
				Finish();
			};

			// Create the first pie chart.
			PlotView view = FindViewById<PlotView>(Resource.Id.plotView);
			view.Model = Diagrams.createPieModel(BrandData);

			// Create the second pie chart.
			PlotView viewTwo = FindViewById<PlotView>(Resource.Id.plotView2);
			viewTwo.Model = DiagramsSecond.createPieModel(ColorData);
		}
	}
}

