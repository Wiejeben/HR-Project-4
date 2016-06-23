
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

namespace Testapplicatie
{
	[Activity(Label = "@string/v4")]
	public class Question4 : Activity
	{
		int counter = 0;

        private PlotModel CreatePlotModel(string title)
		{

            var plotModel = new PlotModel { Title = title };

            var stolenBrandsChart = new PieSeries
            {
                StrokeThickness = 1.0, InsideLabelPosition = 0.5, AngleSpan = 360, StartAngle = 0
            };

			// fake data
			Dictionary<string, int> dicOfValues = new Dictionary<string, int>();

			for (int i = 0; i <= 6; i++)
			{
				counter = counter + 1;
				dicOfValues.Add((counter.ToString()), counter + 3);
			}

			foreach (KeyValuePair<string, int> val in dicOfValues)
			{
				stolenBrandsChart.Slices.Add(new PieSlice(val.Key, val.Value) { IsExploded = true });
			}

            //series1.Slices.Add(new PieSlice("Africa", 1030) { IsExploded = false, Fill = OxyColors.PaleVioletRed });

			plotModel.Series.Add(stolenBrandsChart);

            return plotModel;
        }

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

			PlotView view = FindViewById<PlotView>(Resource.Id.plotView);
			view.Model = CreatePlotModel("Gestolen fietsen op basis van merk");

			PlotView viewTwo = FindViewById<PlotView>(Resource.Id.plotView2);
			viewTwo.Model = CreatePlotModel("Gestolen fietsen op basis van kleur");
		}
	}
}

