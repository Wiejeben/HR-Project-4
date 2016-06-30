using System;
using System.Collections.Generic;
using OxyPlot;

namespace AndroidBicycleInfo
{
	public class Diagram
	{
		private string Title;
		private string XLabel;
		private string YLabel;
		private PlotModel Model;

		public Diagram(string title, string yLabel, string xLabel)
		{
			this.Title = title;
			this.YLabel = yLabel;
			this.XLabel = xLabel;
		}

		public PlotModel CreateBarModel(List<string> x, List<string> y)
		{
			throw new NotImplementedException();
		}

		public PlotModel CreateTwoBarModel(List<string> yLabels, Dictionary<string, int> barOne, Dictionary<string, int> barTwo)
		{
			throw new NotImplementedException();
		}

		public PlotModel CreatePieModel(Dictionary<string, int> data)
		{
			throw new NotImplementedException();
		}

		public PlotModel CreateLineModel(Dictionary<int, int> lineValues, int limit)
		{
			throw new NotImplementedException();
		}
	}
}

