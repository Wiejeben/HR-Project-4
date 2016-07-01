﻿using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using OxyPlot.Xamarin.Android;

namespace AndroidBicycleInfo
{
	[Activity(Label = "@string/us_5")]
	public class BikeTheftsPerMonth : Activity
	{
		// Instance of the diagrams class.
		Diagram Diagram = new Diagram("Gestolen fietsen per maand");

		// fake data
		// {month, value}
		Dictionary<int, int> Data = new Dictionary<int, int>()
		{
			{1 , 50},
			{2 , 75},
			{3 , 20},
			{4 , 12},
			{5 , 30},
			{6 , 50},
			{7 , 75},
			{8 , 20},
			{9 , 12},
			{10 , 30},
			{11 , 12},
			{12 , 30}
		};

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

			// Find the container for our model
            PlotView view = FindViewById<PlotView>(Resource.Id.plotView);
			// Place our created model in the container w/ the values.
			view.Model = this.Diagram.CreateLineModel(
				this.Data, 
				80
			);
        }
    }
}

