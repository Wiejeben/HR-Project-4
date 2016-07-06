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

		public Menu(Activity activity)
		{
            this.Activity = activity;
            Dictionary<Button, Type> ButtonDictionary = new Dictionary<Button, Type>()
            {
                {Activity.FindViewById<Button>(Resource.Id.buttonVraag1), typeof(BikeLocationsActivity) },
                {Activity.FindViewById<Button>(Resource.Id.buttonVraag2), typeof(BikeContainersAndBikeTheftsMenuActivity) },
                {Activity.FindViewById<Button>(Resource.Id.buttonVraag3), typeof(BikeContainerNeighborhoodsActivity) },
                {Activity.FindViewById<Button>(Resource.Id.buttonVraag4), typeof(BikeTheftColorsAndBrandsActivity) },
                {Activity.FindViewById<Button>(Resource.Id.buttonVraag5), typeof(BikeTheftsPerMonthActivity) },
                {Activity.FindViewById<Button>(Resource.Id.buttonVraag6), typeof(BikeAgendaActivity) },
                {Activity.FindViewById<Button>(Resource.Id.buttonVraag8), typeof(RouteCalculatorActivity) },
            };

            this.InitializeMenu(ButtonDictionary);
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

        // Initialize the menu and add the click functions to buttons
        private void InitializeMenu(Dictionary<Button, Type> ButtonDictionary)
        {
            foreach(var entry in ButtonDictionary)
            {
                Option<Button> ButtonOption = new Some<Button>(entry.Key);
                ButtonOption.Visit<Button>(()=> { throw new Exception("Not a button!"); }, 
                    button => { button.Click += delegate { this.StartActivity(entry.Value); }; return button; });
            }
        }

		private void StartActivity(Type type)
		{
			this.Activity.StartActivity(type);
		}
	}
}