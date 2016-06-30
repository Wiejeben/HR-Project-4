using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;

namespace AndroidBicycleInfo
{
    [Activity(Label = "AndroidBicycleInfo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //SetContentView(Resource.Layout.Main);

			Database.Boot(this);
        }
    }
}

