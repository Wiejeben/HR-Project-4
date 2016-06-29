using System;
using SQLite;

namespace AndroidBicycleInfo
{
	[Table("streets")]
	public class Street
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string name { get; set; }
	}
}

