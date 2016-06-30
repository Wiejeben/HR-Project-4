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

namespace AndroidBicycleInfo.Services
{
    class Menu
    {
        private Activity Activity;
        public Menu(Activity activity)
        {
            this.Activity = activity;
        }

        private void StartActivity(Type type)
        {
            this.Activity.StartActivity(type);
        }
    }
}