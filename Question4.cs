
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
	[Activity(Label = "@string/v4")]
	public class Question4 : Activity
	{
		Router router = new Router();

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			// Set layout view.
			SetContentView(Resource.Layout.Question_One);

			// Button & eventhandler.
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

