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
		PlotModel model;

		// HIER KAN EVENTUEEL EEN LIJSTJE LABELS VOOR DE Y-AS KOMEN?!!??!?!

		// Somewhat empty constructor ._.
		public Diagrams(string title = "This is a diagram.", string xAxisLabel = "", string yAxisLabel = "") {
			this.Title = title;
			this.xAxisLabel = xAxisLabel;
			this.yAxisLabel = yAxisLabel;

			this.model = new PlotModel
			{
				Title = this.Title,
				LegendPlacement = LegendPlacement.Outside,
				LegendPosition = LegendPosition.BottomCenter,
				LegendOrientation = LegendOrientation.Horizontal,
				LegendBorderThickness = 0
			};
		} 

		public PlotModel createTwoBarModel(List<string> yAxisLabels, string xAxisBar1Name, List<int> xAxisBar1, string xAxisBar2Name, List<int> xAxisBar2)
		{
			// BEGIN: MODEL::BASE

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
			model.Axes.Add(yAxis);
			model.Axes.Add(xAxis);

			// Add bars to model
			model.Series.Add(firstBar);
			model.Series.Add(secondBar);

			// END MODEL::VISUALIZATION

			return model;
		}

		public PlotModel CreateBarModel(List<string> neighbourhoods, List<int> thefts)
		{
			// BEGIN: MODEL::BASE

			// Axises & their properties
			var yAxis = new CategoryAxis { Title = yAxisLabel, Position = AxisPosition.Left, IsPanEnabled = false, IsZoomEnabled = false };
			var xAxis = new LinearAxis { Title = xAxisLabel, Position = AxisPosition.Bottom, Minimum = 0, AbsoluteMinimum = 0 };

			// Bars & their properties
			var neighbourhoodBar_1 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };
			var neighbourhoodBar_2 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };
			var neighbourhoodBar_3 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };
			var neighbourhoodBar_4 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };
			var neighbourhoodBar_5 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };

			// END: MODEL::BASE

			// BEGIN: MODEL::VALUES

			foreach (string neighbourhood in neighbourhoods)
			{
				// For the sidebar
				yAxis.Labels.Add(neighbourhood);
			}

			// The bars shown value
			foreach (int theft in thefts)
			{
				neighbourhoodBar_1.Items.Add(new BarItem { Value = theft });
			}

			// END: MODEL::VALUES

			// BEGIN: MODEL::VISUALIZATION

			// Add the bars to the model.
			model.Series.Add(neighbourhoodBar_1);
			model.Series.Add(neighbourhoodBar_2);
			model.Series.Add(neighbourhoodBar_3);
			model.Series.Add(neighbourhoodBar_4);
			model.Series.Add(neighbourhoodBar_5);

			// Add the axes to the model.
			model.Axes.Add(yAxis);
			model.Axes.Add(xAxis);

			// END: MODEL::VISUALIZATION

			return model;
		}

	}
}

