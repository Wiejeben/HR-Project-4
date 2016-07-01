using Android;
using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;

namespace AndroidBicycleInfo
{
	[Activity(Label = "Main Menu", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{

			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			Database.Boot(this);

			// Back button
			Button returnButton = FindViewById<Button>(Resource.Id.returnButton);

			if (returnButton != null)
			{
				returnButton.Click += delegate
				{
					StartActivity(typeof(MainActivity));

					Finish();
				};
			}

            Menu menu = new Menu(this);
		}
	}
}


