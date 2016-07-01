using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;

using OxyPlot.Xamarin.Android;

namespace AndroidBicycleInfo
{
	[Activity(Label = "@string/us_2")]
	public class BikeContainersAndBikeThefts : Activity
	{
		// Months
		List<string> months = new List<string>(
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

		// Fake data
		List<int> firstBar = new List<int>(
			new int[] {
			100,200,300,400,500,600,700,800,900,1000,1100,1200
		});

		List<int> secondBar = new List<int>(
			new int[] {
			50,150,250,350,450,550,650,750,850,950,1050,1150
		});

		// Diagram class instance
		Diagrams Diagrams = new Diagrams("Fietstrommels & fietsdiefstallen per maand");

        protected override void OnCreate(Bundle savedInstanceState)
		{
			// Create the instance on the base
			base.OnCreate(savedInstanceState);
			// Set view.
			SetContentView(Resource.Layout.One_View);

			// Our container for the model
            PlotView view = FindViewById<PlotView>(Resource.Id.plotView);

			// Create the model (diagrams.f) and place it in the view (view.model)
			view.Model = Diagrams.createTwoBarModel(
				months,
				"Geinstalleerde fietstrommels",
				firstBar,
				"Fietsdiefstallen",
				secondBar
			);

			// Return button & eventhandler.
			Button returnButton = FindViewById<Button>(Resource.Id.returnButton);
			returnButton.Click += delegate
			{
				// Swap to the right activity.
				StartActivity(typeof(MainActivity));
				// Close the current layout.
				Finish();
			};


        }
	}
}

