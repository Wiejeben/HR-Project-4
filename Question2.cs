
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
	[Activity(Label = "@string/v2")]
	public class Question2 : Activity
	{
        private PlotModel CreatePlotModel()
        {

            var plotModel = new PlotModel
            {
                Title = "BarSeries",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0
            };


            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };
            categoryAxis.Labels.Add("Januari");
            categoryAxis.Labels.Add("Februari");
            categoryAxis.Labels.Add("Maart");
            categoryAxis.Labels.Add("April");
            categoryAxis.Labels.Add("Mei");
            categoryAxis.Labels.Add("Juni");
            categoryAxis.Labels.Add("Juli");
            categoryAxis.Labels.Add("Augustus");
            categoryAxis.Labels.Add("September");
            categoryAxis.Labels.Add("Oktober");
            categoryAxis.Labels.Add("November");
            categoryAxis.Labels.Add("December");
            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };


            var s1 = new BarSeries { Title = "Geïnstalleerde fietstrommels", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            s1.Items.Add(new BarItem { Value = 25 });
            s1.Items.Add(new BarItem { Value = 137 });
            s1.Items.Add(new BarItem { Value = 18 });
            s1.Items.Add(new BarItem { Value = 40 });
            s1.Items.Add(new BarItem { Value = 41 });
            s1.Items.Add(new BarItem { Value = 42 });
            s1.Items.Add(new BarItem { Value = 43 });
            s1.Items.Add(new BarItem { Value = 44 });
            s1.Items.Add(new BarItem { Value = 45 });
            s1.Items.Add(new BarItem { Value = 46 });
            s1.Items.Add(new BarItem { Value = 47 });
            s1.Items.Add(new BarItem { Value = 48 });


            var s2 = new BarSeries { Title = "Fietsdiefstallen", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            s2.Items.Add(new BarItem { Value = 12 });
            s2.Items.Add(new BarItem { Value = 14 });
            s2.Items.Add(new BarItem { Value = 120 });
            s2.Items.Add(new BarItem { Value = 26 });
            s2.Items.Add(new BarItem { Value = 27 });
            s2.Items.Add(new BarItem { Value = 28 });
            s2.Items.Add(new BarItem { Value = 29 });
            s2.Items.Add(new BarItem { Value = 22 });
            s2.Items.Add(new BarItem { Value = 21 });
            s2.Items.Add(new BarItem { Value = 22 });
            s2.Items.Add(new BarItem { Value = 23 });
            s2.Items.Add(new BarItem { Value = 24 });

            plotModel.Series.Add(s1);
            plotModel.Series.Add(s2);
            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);

            return plotModel;
        }


        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			// Set view.
			SetContentView(Resource.Layout.Question_Two);

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

