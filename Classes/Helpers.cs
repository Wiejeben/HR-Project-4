using System;
using Java.Util;

namespace Testapplicatie
{
	public class Helpers
	{
		public Helpers()
		{
		}

		public static long convertToMilliseconds(int yr, int month, int day, int hr = 0, int min = 0)
		{
			Calendar calendar = Calendar.GetInstance(Java.Util.TimeZone.Default);

			calendar.Set(Calendar.Year, yr);
			calendar.Set(Calendar.Month, month);
			calendar.Set(Calendar.DayOfMonth, day);
			calendar.Set(Calendar.HourOfDay, hr);
			calendar.Set(Calendar.Minute, min);

			return calendar.TimeInMillis;
		}
	}
}

