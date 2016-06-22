using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Testapplicatie
{
	[Activity(Label = "Main Menu", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it

			Button firstQuestion = FindViewById<Button>(Resource.Id.buttonVraag1);
			Button secondQuestion = FindViewById<Button>(Resource.Id.buttonVraag2);
			Button thirdQuestion = FindViewById<Button>(Resource.Id.buttonVraag3);
			Button fourthQuestion = FindViewById<Button>(Resource.Id.buttonVraag4);
			Button fifthQuestion = FindViewById<Button>(Resource.Id.buttonVraag5);
			Button sixthQuestion = FindViewById<Button>(Resource.Id.buttonVraag6);


			firstQuestion.Click += delegate{
				// Swap to the right activity.
				StartActivity(typeof(Question1));
			};

			secondQuestion.Click += delegate
			{
				// Swap to the right activity.
				StartActivity(typeof(Question2));
			};

			thirdQuestion.Click += delegate
			{
				// Swap to the right activity.
				StartActivity(typeof(Question3));
			};

			fourthQuestion.Click += delegate
			{
				// Swap to the right activity.
				StartActivity(typeof(Question4));
			};

			fifthQuestion.Click += delegate
			{
				// Swap to the right activity.
				StartActivity(typeof(Question5));
			};

			sixthQuestion.Click += delegate
			{
				// Swap to the right activity.
				StartActivity(typeof(Question6));
			};

		}
	}
}


