using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

namespace Testapplicatie
{
	[Activity(Label = "@string/us_4")]
	public class Question4 : Activity
	{
		// Two instances because we need one for every model and the plot model gets created in the constructor.
		// It's impossible to re-use an old plot model.
		Diagrams Diagrams 		= new Diagrams("Gestolen fietsen op basis van merk");
		Diagrams DiagramsSecond = new Diagrams("Gestolen fietsen op basis van kleur");

		// fake data
		Dictionary<string, int> chartValues = new Dictionary<string, int>()
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

			// Set layout view.
			SetContentView(Resource.Layout.Two_Views);

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
			view.Model = Diagrams.createPieModel(chartValues);

			// Create the second pie chart.
			PlotView viewTwo = FindViewById<PlotView>(Resource.Id.plotView2);
			viewTwo.Model = DiagramsSecond.createPieModel(chartValues);
		}
	}
}

