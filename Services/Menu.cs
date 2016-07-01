using Android.App;
using Android.Widget;
using Android.OS;
using System;
using AndroidBicycleInfo;

class Menu
{
    private Activity Activity;

	// List with buttons
	private Button BikeLocationsActivity;
	private Button BikeContainersAndBikeTheftsActivity;
	private Button BikeContainerNeighborhoodsActivity;
	private Button BikeTheftColorsAndBrandsActivity;
	private Button BikeTheftsPerMonthActivity;
	private Button BikeAgendaActivity;
	private Button BikeTheftsPerNeighborhoodActivity;
	private Button RouteCalculatorActivity;

    public Menu(Activity activity)
    {
        this.Activity = activity;

		// Assign element
        this.BikeLocationsActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag1);
		this.BikeContainersAndBikeTheftsActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag2);
		this.BikeContainerNeighborhoodsActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag3);
		this.BikeTheftColorsAndBrandsActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag4);
		this.BikeTheftsPerMonthActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag5);
		this.BikeAgendaActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag6);
		this.BikeTheftsPerNeighborhoodActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag7);
		this.RouteCalculatorActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag8);

		// Assign events
		this.BikeLocationsActivity.Click += delegate { this.LocationCheck(typeof(BikeLocationsActivity)); };
		this.BikeContainersAndBikeTheftsActivity.Click += delegate { this.StartActivity(typeof(BikeContainersAndBikeTheftsActivity)); };
		this.BikeContainerNeighborhoodsActivity.Click += delegate { this.StartActivity(typeof(BikeContainerNeighborhoodsActivity)); };
		this.BikeTheftColorsAndBrandsActivity.Click += delegate { this.StartActivity(typeof(BikeTheftColorsAndBrandsActivity)); };
		this.BikeTheftsPerMonthActivity.Click += delegate { this.StartActivity(typeof(BikeTheftsPerMonthActivity)); };
		this.BikeAgendaActivity.Click += delegate { this.LocationCheck(typeof(BikeAgendaActivity)); };
		this.BikeTheftsPerNeighborhoodActivity.Click += delegate { this.LocationCheck(typeof(BikeTheftsPerNeighborhoodActivity)); };
		this.RouteCalculatorActivity.Click += delegate { this.LocationCheck(typeof(RouteCalculatorActivity)); };
    }

	private void LocationCheck(Type type)
	{
		if (General.LocationStatus(this.Activity))
		{
			this.StartActivity(type);
		}
		else
		{
			Toast.MakeText(this.Activity, "Locatie staat uit", ToastLength.Short).Show();
		}
	}

	private void StartActivity(Type type)
    {
        this.Activity.StartActivity(type);
    }
}
