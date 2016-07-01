using System;
using SQLite;

namespace AndroidBicycleInfo
{
	public class BikeTheft
	{
		[PrimaryKey]
		public int id { get; set; }
		public int date { get; set; }
        public int container { get; set; }

        [Indexed]
		public int month { get; set; }
		public int thefts { get; set; }

		[Indexed]
		public int brand_id { get; set; }

		[Indexed]
		public int color_id { get; set; }

		[Indexed]
		public int street_id { get; set; }
	}
}
