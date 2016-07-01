using System;
using SQLite;

namespace AndroidBicycleInfo
{
	[Table("districts")]
	public class District
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string name { get; set; }
	}
}
