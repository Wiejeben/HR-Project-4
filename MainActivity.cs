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
		protected override void OnCreate(Bundle savedInstanceState)
		{
			/*
			// Impossible to create a dynamically named variable, so we cant loop through a list of buttons unless we find a solution for this.
			Dictionary<string, int> buttonList = new Dictionary<string, int>();
			buttonList.Add("firstQuestion", Resource.Id.buttonVraag1);
			buttonList.Add("secondQuestion", Resource.Id.buttonVraag2);


			foreach (KeyValuePair<string, int> btn in buttonList)
			{
				// Button a = FindViewById<Button>(btn.Value);
			}
			*/

			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

            Menu Menu = new Menu(this);
		}
	}
}


