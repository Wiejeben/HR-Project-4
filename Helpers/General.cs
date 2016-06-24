using System;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;


namespace Testapplicatie
{
	public class General
	{
		// Hide keyboard
		public static void HideKeyboard(Question1 parent)
		{
			View view = parent.CurrentFocus;
			if (view != null)
			{
				InputMethodManager imm = (InputMethodManager)parent.GetSystemService(Context.InputMethodService);
				imm.HideSoftInputFromWindow(view.WindowToken, 0);
			}
		}
	}
}