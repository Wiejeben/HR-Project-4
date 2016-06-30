using System;
using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AndroidBicycleInfo
{
	public class Diagram
	{
		private string Title;
		private string XLabel;
		private string YLabel;
		private PlotModel Model;

		public Diagram(string title = "", string yLabel = "", string xLabel = "")
		{
			this.Title = title;
			this.YLabel = yLabel;
			this.XLabel = xLabel;

			// Create Model instance
			this.Model = new PlotModel
			{
				Title = this.Title,
				LegendPlacement = LegendPlacement.Outside,
				LegendPosition = LegendPosition.BottomCenter,
				LegendOrientation = LegendOrientation.Horizontal,
				LegendBorderThickness = 0
			};
		}

		public PlotModel CreateBarModel(Dictionary<string, int> data)
		{
			CategoryAxis yAxis = new CategoryAxis { Title = this.YLabel, Position = AxisPosition.Left, IsPanEnabled = false, IsZoomEnabled = false };
			LinearAxis xAxis = new LinearAxis { Title = this.XLabel, Position = AxisPosition.Bottom, Minimum = 0, AbsoluteMinimum = 0 };

			// Create bars
			List<BarSeries> bars = new List<BarSeries>();
			for (int i = 1; i < 5; i++)
			{
				bars.Add(new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" });
			}

			// Parse values
			foreach (KeyValuePair<string, int> entry in data)
			{
				yAxis.Labels.Add(entry.Key);
				bars.First().Items.Add(new BarItem { Value = entry.Value });
			}

			bars.ForEach(bar => this.Model.Series.Add(bar));

			// Add the axes to the model.
			this.Model.Axes.Add(xAxis);
			this.Model.Axes.Add(yAxis);

			return this.Model;
		}

		public PlotModel CreateTwoBarModel(List<string> yLabels, string barOneName, List<int> barOne, string barTwoName, List<int> barTwo)
		{

			// Labels
			CategoryAxis yAxis = new CategoryAxis { Title = this.YLabel, Position = AxisPosition.Left, IsZoomEnabled = false, IsPanEnabled = false };
			// Values
			LinearAxis xAxis = new LinearAxis { Title = this.XLabel, Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };

			// Bars
			BarSeries firstBar = new BarSeries { Title = barOneName, StrokeColor = OxyColors.Black, StrokeThickness = 1 };
			BarSeries secondBar = new BarSeries { Title = barTwoName, StrokeColor = OxyColors.Black, StrokeThickness = 1 };

			// Implement labels
			yLabels.ForEach(value => yAxis.Labels.Add(value));

			// Implement bar values
			barOne.ForEach(value => firstBar.Items.Add(new BarItem { Value = value }));
			barTwo.ForEach(value => secondBar.Items.Add(new BarItem { Value = value }));

			// Add axises to model
			this.Model.Axes.Add(yAxis);
			this.Model.Axes.Add(xAxis);

			// Combine bars and add to Model
			this.Model.Series.Add(firstBar);
			this.Model.Series.Add(secondBar);

			return this.Model;
		}

		public PlotModel CreatePieModel(Dictionary<string, int> data)
		{
			// Pie chart properties	
			PieSeries pieChart = new PieSeries
			{
				StrokeThickness = 1.0,
				InsideLabelPosition = 0.5,
				AngleSpan = 360,
				StartAngle = 0
			};

			foreach (KeyValuePair<string, int> entry in data)
			{
				pieChart.Slices.Add(new PieSlice(entry.Key, entry.Value) { IsExploded = true });
			}

			this.Model.Series.Add(pieChart);

			return this.Model;
		}

		public PlotModel CreateLineModel(Dictionary<int, int> lineValues, int limit)
		{
			LineSeries line = new LineSeries
			{
				MarkerType = MarkerType.Circle,
				MarkerSize = 4,
				MarkerStroke = OxyColors.White
			};

			// Axises & their properties
			LinearAxis xAxis = new LinearAxis
			{
				Position = AxisPosition.Bottom,
				Minimum = 0,
				Maximum = 12,
				AbsoluteMinimum = 0,
				Title = this.XLabel,
				MinorTickSize = 1,
				MajorTickSize = 1,
				MinorStep = 1,
				MajorStep = 1,
				IsPanEnabled = false,
				IsZoomEnabled = false
			};

			LinearAxis yAxis = new LinearAxis
			{
				Position = AxisPosition.Left,
				Minimum = 1,
				Maximum = limit,
				AbsoluteMinimum = 1,
				Title = this.YLabel,
				MaximumPadding = 1,
				MinimumPadding = 1
			};

			foreach (KeyValuePair<int, int> lineVal in lineValues)
			{
				line.Points.Add(new DataPoint(lineVal.Key, lineVal.Value));
			}

			this.Model.Axes.Add(xAxis);
			this.Model.Axes.Add(yAxis);
			this.Model.Series.Add(line);

			return this.Model;
		}
	}
}

