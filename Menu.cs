using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;
using Testapplicatie;

class Menu
{
    private Activity Activity;

    public Menu(Activity activity)
    {
        this.Activity = activity;
    }

    public void QuestionOne()
    {
        this.Activity.StartActivity(typeof(Question1));
    }

    public void QuestionTwo()
    {
        this.Activity.StartActivity(typeof(Question2));
    }

    public void QuestionThree()
    {
        this.Activity.StartActivity(typeof(Question3));
    }

    public void QuestionFour()
    {
        this.Activity.StartActivity(typeof(Question4));
    }
}