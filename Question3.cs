﻿
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
		// Months
		List<string> neighbourhoods = new List<string>(
			new string[] {
				"Wijk 1",
				"Wijk 2",
				"Wijk 3",
				"Wijk 4",
				"Wijk 5",
		});

		// Fake data
		List<int> thefts = new List<int>(
			new int[] {
			100,200,300,400,500
		});

		Diagrams Diagrams = new Diagrams("Top 5 wijken met de meeste fietstrommels", "Hoeveelheid fietstrommels", "Wijken");

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
			view.Model = Diagrams.CreateBarModel(
				neighbourhoods,
				thefts
			);
        }
	}
}
