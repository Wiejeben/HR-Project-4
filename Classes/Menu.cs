using Android.App;
using Android.Widget;
using Android.OS;
using System;
using AndroidBicycleInfo;

class Menu
{
    private Activity Activity;

    public Menu(Activity activity)
    {
        this.Activity = activity;
        Button BikeLocationsActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag1);
        Button BikeContainersAndBikeTheftsActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag2);
        Button BikeContainerNeighborhoodsActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag3);
        Button BikeTheftColorsAndBrandsActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag4);
        Button BikeTheftsPerMonthActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag5);
        Button BikeAgendaActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag6);
		Button BikeTheftsPerNeighborhoodActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag7);
		Button RouteCalculatorActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag8);


		BikeLocationsActivity.Click += delegate {
			if (General.LocationStatus(activity))
			{
				this.StartActivity(typeof(BikeLocations));
			}
			else {
				Toast.MakeText(activity, "Locatie staat uit", ToastLength.Long).Show();
			}
		};

		BikeContainersAndBikeTheftsActivity.Click += delegate { this.StartActivity(typeof(BikeContainersAndBikeThefts)); };
		BikeContainerNeighborhoodsActivity.Click += delegate { this.StartActivity(typeof(BikeContainersNeighborhoods)); };
		BikeTheftColorsAndBrandsActivity.Click += delegate { this.StartActivity(typeof(BikeTheftColorsAndBrands)); };
		BikeTheftsPerMonthActivity.Click += delegate { this.StartActivity(typeof(BikeTheftsPerMonth)); };

		BikeAgendaActivity.Click += delegate
		{
			if (General.LocationStatus(activity))
			{
				this.StartActivity(typeof(BikeAgenda));
			}
			else {
				Toast.MakeText(activity, "Locatie staat uit", ToastLength.Long).Show();
			}
		};

		BikeTheftsPerNeighborhoodActivity.Click += delegate {
			if (General.LocationStatus(activity))
			{
				this.StartActivity(typeof(BikeTheftsPerNeighborhood));
			}
			else {
				Toast.MakeText(activity, "Locatie staat uit", ToastLength.Long).Show();
			}
		};

		RouteCalculatorActivity.Click += delegate
		{
			if (General.LocationStatus(activity))
			{
				this.StartActivity(typeof(RouteCalculator));
			}
			else {
				Toast.MakeText(activity, "Locatie staat uit", ToastLength.Long).Show();
			}
		};
    }

    // Start the activity which has been coupled with the right class
    public void StartActivity(Type type)
    {
        this.Activity.StartActivity(type);
    }
}
