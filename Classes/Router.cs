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
	public class Router : Activity
	{
		public Router()
		{
			
		}

		public void getView(Context old, Type viewActivity)
		{
			//Intent it = new Intent(old, viewActivity);
			//StartActivity(it);
			//StartActivity(viewActivity);
		}

		public void initializeView()
		{
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

		public void closeView()
		{
			// Swap to the right activity.
			StartActivity(typeof(MainActivity));
			// Close the current layout.
			Finish();
		}

	}
}

