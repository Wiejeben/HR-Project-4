
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


            var serie_one = new BarSeries { Title = "Geïnstalleerde fietstrommels", StrokeColor = OxyColors.Black, StrokeThickness = 1 };

            for(int i = 0; i < 11; i++)
                serie_one.Items.Add(new BarItem { Value = i + i + i });


            var serie_two = new BarSeries { Title = "Fietsdiefstallen", StrokeColor = OxyColors.Black, StrokeThickness = 1 };

            for (int i = 0; i < 11; i++)
                serie_two.Items.Add(new BarItem { Value = i + i });
            

            plotModel.Series.Add(serie_one);
            plotModel.Series.Add(serie_two);
            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);

            return plotModel;
        }


        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			// Set view.
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
            var plotView = new PlotView(this);
            plotView.Model = CreatePlotModel();

            this.AddContentView(plotView,
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
        }
	}
}

