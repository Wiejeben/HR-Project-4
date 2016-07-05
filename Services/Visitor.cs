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
    interface OptionVisitor<T, U>
    {
        U onSome(T value);
        U onNone();
    }

    interface Option<T>
    {
        U Visit<U>(Func<U> onNone, Func<T, U> onSome);
    }

    public class Some<T> : Option<T>
    {
        T value;

        public Some(T value)
        {
            this.value = value;
        }

        public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
        {
            return onSome(value);
        }
    }

    class None<T> : Option<T>
    {
        public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
        {
            return onNone();
        }
    }
}