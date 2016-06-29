using System;
using SQLite;

namespace AndroidBicycleInfo
{
	public class BikeTheft
	{
		[PrimaryKey]
		public int id { get; set; }
		public int date { get; set; }

		[Indexed]
		public int brand_id { get; set; }

		[Indexed]
		public int color_id { get; set; }

		[Indexed]
		public int street_id { get; set; }
	}
}

