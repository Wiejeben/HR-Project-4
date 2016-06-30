using System;
using SQLite;

namespace Testapplicatie
{
	public class BikeContainer
	{
		[PrimaryKey]
		public int id { get; set; }
		public float lat { get; set; }
		public float lon { get; set; }

		[Indexed]
		public int street_id { get; set; }
	}
}
