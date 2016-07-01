using System;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

namespace AndroidBicycleInfo
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

		public PlotModel createBarModel(List<string> labels, List<int> values)
		{
			// BEGIN: MODEL::BASE

			// Axises & their properties
			var yAxis = new CategoryAxis { Title = yAxisLabel, Position = AxisPosition.Left, IsPanEnabled = false, IsZoomEnabled = false };
			var xAxis = new LinearAxis { Title = xAxisLabel, Position = AxisPosition.Bottom, Minimum = 0, AbsoluteMinimum = 0 };

			// Bars & their properties
			var Bar_1 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };
			var Bar_2 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };
			var Bar_3 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };
			var Bar_4 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };
			var Bar_5 = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };

			// END: MODEL::BASE

			// BEGIN: MODEL::VALUES

			foreach (string label in labels)
			{
				// For the sidebar
				yAxis.Labels.Add(label);
			}

			// The bars shown value
			foreach (int val in values)
			{
				Bar_1.Items.Add(new BarItem { Value = val });
			}

			// END: MODEL::VALUES

			// BEGIN: MODEL::VISUALIZATION

			// Add the bars to the model.
			model.Series.Add(Bar_1);
			model.Series.Add(Bar_2);
			model.Series.Add(Bar_3);
			model.Series.Add(Bar_4);
			model.Series.Add(Bar_5);

			// Add the axes to the model.
			model.Axes.Add(yAxis);
			model.Axes.Add(xAxis);

			// END: MODEL::VISUALIZATION

			return model;
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

		public PlotModel createPieModel(Dictionary<string, int> slicesData)
		{
			// BEGIN: MODEL::BASE

			// Pie chart properties	
			var pieChart = new PieSeries
			{
				StrokeThickness = 1.0,
				InsideLabelPosition = 0.5,
				AngleSpan = 360,
				StartAngle = 0
			};

			// END: MODEL::BASE

			// BEGIN: MODEL::VALUES

			foreach (KeyValuePair<string, int> val in slicesData)
			{
				pieChart.Slices.Add(new PieSlice(val.Key, val.Value) { IsExploded = true });
			}

			// END: MODEL::VALUES

			// BEGIN: MODEL::VISUALIZATION

			model.Series.Add(pieChart);

			// END: MODEL::VISUALIZATION

			return model;
		}

		public PlotModel createLineModel(Dictionary<int, int> lineValues , int limit)
		{
			// BEGIN: MODEL::BASE

			var line = new LineSeries
			{
				MarkerType = MarkerType.Circle,
				MarkerSize = 4,
				MarkerStroke = OxyColors.White
			};

			// Axises & their properties
			var xAxis = new LinearAxis
			{
				Position = AxisPosition.Bottom, 
				Minimum = 0, Maximum = 12, AbsoluteMinimum = 0,
				Title = xAxisLabel,
				MinorTickSize = 1,MajorTickSize = 1,
				MinorStep = 1, MajorStep = 1,
				IsPanEnabled = false, IsZoomEnabled = false
			};

			var yAxis = new LinearAxis
			{
				Position = AxisPosition.Left,
				Minimum = 1, Maximum = limit, AbsoluteMinimum = 1,
				Title = yAxisLabel,
				MaximumPadding = 1, MinimumPadding = 1
			};

			// END: MODEL::BASE

			// BEGIN: MODEL::VALUES

			foreach (KeyValuePair<int, int> lineVal in lineValues)
			{
				line.Points.Add(new DataPoint(lineVal.Key, lineVal.Value));
			}

			// END: MODEL::VALUES

			// BEGIN: MODEL::VISUALIZATION

			model.Axes.Add(xAxis);
			model.Axes.Add(yAxis);
			model.Series.Add(line);

			// END: MODEL::VISUALIZATION

			return model;
		}

	}
}

