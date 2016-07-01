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

		// fake data
		// {name, value}
		Dictionary<string, int> Data = new Dictionary<string, int>()
		{
			{"Een"  , 50},
			{"Twee" , 75},
			{"Drie" , 20},
			{"Vier" , 12},
			{"Vijf" , 30},
		};

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Two_Views);

			// Create thefts by brand diagram
			PlotView view = FindViewById<PlotView>(Resource.Id.plotView);
			view.Model = TheftByBrand.CreatePieModel(this.Data);

			// Create thefts by color diagram
			PlotView viewTwo = FindViewById<PlotView>(Resource.Id.plotView2);
			viewTwo.Model = TheftByColor.CreatePieModel(this.Data);

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

