
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Renderscripts;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

namespace Testapplicatie
{
	[Activity(Label = "@string/v3")]
	public class Question3 : Activity
	{
        private PlotModel CreatePlotModel()
        {
            var plotModel = new PlotModel { 
				Title = "Top 5 wijken met de meeste fietstrommels",
				LegendPlacement = LegendPlacement.Outside,
				LegendPosition = LegendPosition.BottomCenter,
				LegendOrientation = LegendOrientation.Horizontal
			};
            
            var yAxis = new CategoryAxis { Position = AxisPosition.Left };
			var xAxis = // X-axis attributes
			new LinearAxis
			{
				Position = AxisPosition.Bottom,
				Minimum = 0,
				AbsoluteMinimum = 0,
				Title = "Hoeveelheid fietstrommels"
			};

			yAxis.IsZoomEnabled = false;
			yAxis.IsPanEnabled = false;

			List<KeyValuePair<string, double>> neighbourhoods = new List<KeyValuePair<string, double>>();
			// Test data.
			neighbourhoods.Add(new KeyValuePair<string, double>("Buurt 1", 100)); neighbourhoods.Add(new KeyValuePair<string, double>("Buurt 2", 200)); neighbourhoods.Add(new KeyValuePair<string, double>("Buurt 3", 300)); neighbourhoods.Add(new KeyValuePair<string, double>("Buurt 4", 400)); neighbourhoods.Add(new KeyValuePair<string, double>("Buurt 5", 500));

			foreach (KeyValuePair<string, double> neighbourhood in neighbourhoods)
			{
				// For the sidebar
				yAxis.Labels.Add(neighbourhood.Key);
			}

			// The bars attributes
			var neighbourhoodBar_1 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };
			var neighbourhoodBar_2 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };
			var neighbourhoodBar_3 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };
			var neighbourhoodBar_4 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };
			var neighbourhoodBar_5 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };

			// The bars shown value
			neighbourhoodBar_1.Items.Add(new BarItem { Value = neighbourhoods[0].Value });
			neighbourhoodBar_1.Items.Add(new BarItem { Value = neighbourhoods[1].Value });
			neighbourhoodBar_1.Items.Add(new BarItem { Value = neighbourhoods[2].Value });
			neighbourhoodBar_1.Items.Add(new BarItem { Value = neighbourhoods[3].Value });
			neighbourhoodBar_1.Items.Add(new BarItem { Value = neighbourhoods[4].Value });

			// Add the bars to the model.
			plotModel.Series.Add(neighbourhoodBar_1);
			plotModel.Series.Add(neighbourhoodBar_2);
			plotModel.Series.Add(neighbourhoodBar_3);
			plotModel.Series.Add(neighbourhoodBar_4);
			plotModel.Series.Add(neighbourhoodBar_5);

			// Add the axes to the model.
			plotModel.Axes.Add(yAxis);
            plotModel.Axes.Add(xAxis);

            return plotModel;
        }

        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			// Set layout view.
			SetContentView(Resource.Layout.One_View);

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
			view.Model = CreatePlotModel();
        }
	}
}

