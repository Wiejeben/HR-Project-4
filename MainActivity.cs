using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;

namespace Testapplicatie
{
	[Activity(Label = "Main Menu", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			Dictionary<string, int> buttonList = new Dictionary<string, int>();
			buttonList.Add("firstQuestion", Resource.Id.buttonVraag1);

			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button firstQuestion = FindViewById<Button>(Resource.Id.buttonVraag1);
			Button secondQuestion = FindViewById<Button>(Resource.Id.buttonVraag2);



			firstQuestion.Click += delegate{
				// Swap to the right activity.
				StartActivity(typeof(Question1));
			};

			secondQuestion.Click += delegate
			{
				// Swap to the right activity.
				StartActivity(typeof(Question2));
			};

		}
	}
}


