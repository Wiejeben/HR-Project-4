using System;
using SQLite;

namespace AndroidBicycleInfo
{
	public class BikeContainer
	{
		[PrimaryKey]
		public int id { get; set; }
		public double lat { get; set; }
		public double lon { get; set; }

		[Indexed]
		public int street_id { get; set; }
	}
}
