using System;
using Java.Util;

namespace Testapplicatie
{
	public class Helpers
	{
		public Helpers()
		{
		}

		public static long convertToMilliseconds(int yr, int month, int day, int hr, int min)
		{
			Calendar c = Calendar.GetInstance(Java.Util.TimeZone.Default);

			c.Set(Calendar.DayOfMonth, day);
			c.Set(Calendar.HourOfDay, hr);
			c.Set(Calendar.Minute, min);
			c.Set(Calendar.Month, month);
			c.Set(Calendar.Year, yr);

			return c.TimeInMillis;
		}
	}
}

