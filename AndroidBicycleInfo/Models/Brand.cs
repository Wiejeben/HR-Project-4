using System;
using SQLite;

namespace AndroidBicycleInfo
{
	[Table("brands")]
	public class Brand
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string name { get; set; }
	}
}

