
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	[Activity(Label = "@string/v5")]
	public class Question5 : Activity
	{
		Router router = new Router();

		int highestAmount = 12; // Dynamically get the highest amount of bikes stolen.


		// Temporary 'lorem ipsum' chart attributes
		int counter = 0;

        private PlotModel CreatePlotModel()
        {

			// BASIC MODEL SETUP STARTS HERE
            var plotModel = new PlotModel { Title = "Gestolen fietsen per maand" }; // Model title.


			var xAxis = // X-axis attributes
			new LinearAxis
			{
				Position = AxisPosition.Bottom,
				Minimum = 0,
				Maximum = 12,
				AbsoluteMinimum = 0,
				Title = "Maanden",
				MinorTickSize = 1,
				MajorTickSize = 1,
				MinorStep = 1,
				MajorStep = 1
			};


			var yAxis = // Y-axis attributes
			new LinearAxis
			{
				Position = AxisPosition.Left,
				Minimum = 1,
				Maximum = highestAmount,
				AbsoluteMinimum = 1,
				Title = "Hoeveelheid fietsen",
				MaximumPadding = 1,
				MinimumPadding = 1
			};

			// Disable moving the screen on the x-axis, so that we don't see more than 12 months.
			xAxis.IsPanEnabled = false;
			xAxis.IsZoomEnabled = false;

			// X and Y-axis added to the model.
			plotModel.Axes.Add(xAxis);
			plotModel.Axes.Add(yAxis); 

			// Settings for the line chart.
            var line = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };

			// BASIC MODEL SETUP ENDS HERE

			Dictionary<int, int> dicOfValues = new Dictionary<int, int>();

			for (int i = 0; i <= 10; i++)
			{
				counter = counter + 1;
				dicOfValues.Add(counter, counter + 3);
			}

			foreach (KeyValuePair<int, int> val in dicOfValues)
			{
				line.Points.Add(new DataPoint(val.Key, val.Value));
			}

            plotModel.Series.Add(line);

            return plotModel;
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set layout view.
            SetContentView(Resource.Layout.Question_One);

            // Button & eventhandler.
            Button returnButton = FindViewById<Button>(Resource.Id.returnButton);
            returnButton.Click += delegate
            {
                // Swap to the right activity.
                StartActivity(typeof(MainActivity));
                // Close the current layout.
                Finish();
            };

            var plotView = new PlotView(this);
            plotView.Model = CreatePlotModel();

            this.AddContentView(plotView,
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
        }
    }
}

