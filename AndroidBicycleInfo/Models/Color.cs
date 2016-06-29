using System;
using SQLite;

namespace AndroidBicycleInfo
{
	[Table("colors")]
	public class Color
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string name { get; set; }
	}
}

