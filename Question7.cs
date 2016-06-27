﻿
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

namespace Testapplicatie
{
	[Activity(Label = "@string/us_7")]
	public class Question7 : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			// Create the instance on the base
			base.OnCreate(savedInstanceState);
			// Set view.
			SetContentView(Resource.Layout.One_View);

			// Return button & eventhandler.
			Button returnButton = FindViewById<Button>(Resource.Id.returnButton);
			returnButton.Click += delegate
			{
				// Swap to the right activity.
				StartActivity(typeof(MainActivity));
				// Close the current layout.
				Finish();
			};
		}
	}
}

