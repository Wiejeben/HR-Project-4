using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;
using Testapplicatie;

class Menu
{
    private Activity Activity;
    private List<Button> ButtonList;

    public Menu(Activity activity)
    {
        this.Activity = activity;
        Button firstQuestion = Activity.FindViewById<Button>(Resource.Id.buttonVraag1);
        Button secondQuestion = Activity.FindViewById<Button>(Resource.Id.buttonVraag2);
        Button thirdQuestion = Activity.FindViewById<Button>(Resource.Id.buttonVraag3);
        Button fourthQuestion = Activity.FindViewById<Button>(Resource.Id.buttonVraag4);
        Button fifthQuestion = Activity.FindViewById<Button>(Resource.Id.buttonVraag5);
        Button sixthQuestion = Activity.FindViewById<Button>(Resource.Id.buttonVraag6);

        firstQuestion.Click += delegate { this.StartActivity(typeof(Question1)); };
        secondQuestion.Click += delegate { this.StartActivity(typeof(Question2)); };
        thirdQuestion.Click += delegate { this.StartActivity(typeof(Question3)); };
        fourthQuestion.Click += delegate { this.StartActivity(typeof(Question4)); };
        fifthQuestion.Click += delegate { this.StartActivity(typeof(Question5)); };
        sixthQuestion.Click += delegate { this.StartActivity(typeof(Question6)); };
    }

    // Start the activity which has been coupled with the right class
    public void StartActivity(Type type)
    {
        this.Activity.StartActivity(type);
    }
}
