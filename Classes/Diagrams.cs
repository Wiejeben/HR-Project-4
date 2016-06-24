using System;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

namespace Testapplicatie
{
	public class Diagrams
	{
		// Default values.
		string Title;
		string xAxisLabel;
		string yAxisLabel;
		// HIER KAN EVENTUEEL EEN LIJSTJE LABELS VOOR DE Y-AS KOMEN?!!??!?!

		// Somewhat empty constructor ._.
		public Diagrams(string title = "This is a diagram.", string xAxisLabel = "", string yAxisLabel = "") {
			this.Title = title;
			this.xAxisLabel = xAxisLabel;
			this.yAxisLabel = yAxisLabel;
		} 

		public PlotModel createBarModel(List<string> yAxisLabels, string xAxisBar1Name, List<int> xAxisBar1, string xAxisBar2Name, List<int> xAxisBar2)
		{
			// BEGIN: MODEL::BASE

			// Model properties
			var barModel = new PlotModel
			{
				Title = this.Title,
				LegendPlacement = LegendPlacement.Outside,
				LegendPosition = LegendPosition.BottomCenter,
				LegendOrientation = LegendOrientation.Horizontal,
				LegendBorderThickness = 0
			};

			// Axises & their properties
			var yAxis = new CategoryAxis { Title = yAxisLabel, Position = AxisPosition.Left, IsZoomEnabled = false, IsPanEnabled = false };
			var xAxis = new LinearAxis { Title = xAxisLabel, Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };

			// Bars & their properties
			var firstBar = new BarSeries { Title = xAxisBar1Name, StrokeColor = OxyColors.Black, StrokeThickness = 1 };
			var secondBar = new BarSeries { Title = xAxisBar2Name, StrokeColor = OxyColors.Black, StrokeThickness = 1 };

			// END: MODEL::BASE

			// BEGIN: MODEL::VALUES

			// Add Y-AXIS labels
			foreach (string yAxisL in yAxisLabels)
			{
				yAxis.Labels.Add(yAxisL);
			}

			foreach (var xVal1 in xAxisBar1)
			{
				firstBar.Items.Add(new BarItem { Value = xVal1 });
			}

			foreach (var xVal2 in xAxisBar2)
			{
				secondBar.Items.Add(new BarItem { Value = xVal2 });
			}

			// END: MODEL::VALUES

			// BEGIN MODEL::VISUALIZATION

			// Add axises to model
			barModel.Axes.Add(yAxis);
			barModel.Axes.Add(xAxis);

			// Add bars to model
			barModel.Series.Add(firstBar);
			barModel.Series.Add(secondBar);

			// END MODEL::VISUALIZATION

			return barModel;
		}

	}
}

