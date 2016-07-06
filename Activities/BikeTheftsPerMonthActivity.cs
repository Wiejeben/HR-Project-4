using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using OxyPlot.Xamarin.Android;

namespace AndroidBicycleInfo
{
	[Activity(Label = "@string/us_5")]
	public class BikeTheftsPerMonthActivity : MainActivity
	{
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			SetContentView(Resource.Layout.BikeTheft);
			this.registerReturnButton();

			Button sreturnButton = FindViewById<Button>(Resource.Id.sreturnButton);
			// Return to the menu button & it's event handler.
			sreturnButton.Click += delegate
			{
				StartActivity(typeof(BikeTheftMenuActivity));
				Finish();
			};

			string year = Intent.GetStringExtra("year");

			Dictionary<int, int> Data = new Dictionary<int, int>();
			// Query & getting the results.
			var db = Database.Load();
			string BikeTheftsQuery = "Select count(*) as thefts, strftime('%m', date, 'unixepoch') as month, strftime('%Y', date, 'unixepoch')  as year FROM bikethefts WHERE year = '"+year+"' GROUP BY month";
			var results = db.Query<BikeTheft>(BikeTheftsQuery);
			results.ForEach(value => Data.Add(value.month, value.thefts));

			Diagram diagram = new Diagram("Gestolen fietsen per maand in Rotterdam in " + year);
            PlotView view = FindViewById<PlotView>(Resource.Id.plotView);
			view.Model = diagram.CreateLineModel(
				Data, 
				25,
				"Maand",
				"Diefstallen"
			);
        }
    }
}

