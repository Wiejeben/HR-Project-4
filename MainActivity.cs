using Android;
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

			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

            Menu Menu = new Menu(this);

			Database.Boot(this);

			var db = Database.Load();

			string TopContainersQuery = "SELECT b.id, s.name, COUNT(*) as drums FROM bikecontainers as b INNER JOIN streets as s ON s.id = b.street_id GROUP BY street_id ORDER BY drums DESC LIMIT 5";
			var result = db.Query<Street>(TopContainersQuery);

			foreach (Street entry in result)
			{
				var x = result;
				Toast.MakeText(this, entry.name, ToastLength.Short).Show();
			}
		}
	}
}


