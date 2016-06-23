
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var plotModel = new PlotModel { Title = "Pie Sample1" };
            
            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };
            categoryAxis.Labels.Add("Buurt 1");
            categoryAxis.Labels.Add("Buurt 2");
            categoryAxis.Labels.Add("Buurt 3");
            categoryAxis.Labels.Add("Buurt 4");
            categoryAxis.Labels.Add("Buurt 5");

            var rand = new Random();
            double[] cakePopularity = new double[5];
            for (int i = 0; i < 5; ++i)
            {
                cakePopularity[i] = rand.NextDouble();
            }
            var sum = cakePopularity.Sum();

            var series1 = new BarSeries
            {
                Title = "Fietstrommels",
                ItemsSource = new List<BarItem>(new[]
                {
                    new BarItem{ Value = (cakePopularity[0] / sum * 100) },
                    new BarItem{ Value = (cakePopularity[1] / sum * 100) },
                    new BarItem{ Value = (cakePopularity[2] / sum * 100) },
                    new BarItem{ Value = (cakePopularity[3] / sum * 100) },
                    new BarItem{ Value = (cakePopularity[4] / sum * 100) }
                 }),
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0:.00}%"
            };

            plotModel.Series.Add(series1);
            plotModel.Axes.Add(categoryAxis);

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

