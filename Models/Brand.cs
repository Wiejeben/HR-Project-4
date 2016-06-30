using System;
using SQLite;

namespace Testapplicatie
{
	[Table("brands")]
	public class Brand
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string name { get; set; }
	}
}
