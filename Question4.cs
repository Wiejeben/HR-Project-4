
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
	[Activity(Label = "@string/v4")]
	public class Question4 : Activity
	{

        private PlotModel CreatePlotModel()
        {

            var plotModel = new PlotModel { Title = "Pie Sample1" };

            var series1 = new PieSeries
            {
                StrokeThickness = 2.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0
            };



            series1.Slices.Add(new PieSlice("Africa", 1030) { IsExploded = false, Fill = OxyColors.PaleVioletRed });
            series1.Slices.Add(new PieSlice("Americas", 929) { IsExploded = true });
            series1.Slices.Add(new PieSlice("Asia", 4157) { IsExploded = true });
            series1.Slices.Add(new PieSlice("Europe", 739) { IsExploded = true });
            series1.Slices.Add(new PieSlice("Oceania", 35) { IsExploded = true });

            plotModel.Series.Add(series1);


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

