using Android.App;
using Android.Widget;
using Android.OS;
using System;
using AndroidBicycleInfo;
using System.Collections.Generic;

namespace AndroidBicycleInfo
{
	class Menu
	{
		private Activity Activity;
        private List<Button> ButtonList;

		public Menu(Activity activity)
		{
			this.Activity = activity;

			// Assign events
			bikeLocationsActivity.Click += delegate { this.LocationCheck(typeof(BikeLocationsActivity)); };
			bikeContainersAndBikeTheftsActivity.Click += delegate { this.StartActivity(typeof(BikeContainersAndBikeTheftsActivity)); };
			bikeContainerNeighborhoodsActivity.Click += delegate { this.StartActivity(typeof(BikeContainerNeighborhoodsActivity)); };
			bikeTheftColorsAndBrandsActivity.Click += delegate { this.StartActivity(typeof(BikeTheftColorsAndBrandsActivity)); };
			bikeTheftsPerMonthActivity.Click += delegate { this.StartActivity(typeof(BikeTheftsPerMonthActivity)); };
			bikeAgendaActivity.Click += delegate { this.LocationCheck(typeof(BikeAgendaActivity)); };
			bikeTheftsPerNeighborhoodActivity.Click += delegate { this.LocationCheck(typeof(BikeTheftsPerNeighborhoodActivity)); };
			routeCalculatorActivity.Click += delegate { this.LocationCheck(typeof(RouteCalculatorActivity)); };
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

        private void InitializeMenu()
        {
            // Assign element
            Button bikeLocationsActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag1);
            Button bikeContainersAndBikeTheftsActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag2);
            Button bikeContainerNeighborhoodsActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag3);
            Button bikeTheftColorsAndBrandsActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag4);
            Button bikeTheftsPerMonthActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag5);
            Button bikeAgendaActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag6);
            Button bikeTheftsPerNeighborhoodActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag7);
            Button routeCalculatorActivity = Activity.FindViewById<Button>(Resource.Id.buttonVraag8);

            foreach(Button button in ButtonList)
            {
                Services.Option<Button> ButtonOption = new Services.Some<Button>(button);
                ButtonOption.Visit<object>(()=> { throw new Exception(""); }, 
                    btn => { return btn.Click += delegate { this.StartActivity(typeof(BikeLocationsActivity));});
            }
        }

		private void StartActivity(Type type)
		{
			this.Activity.StartActivity(type);
		}
	}
}