using System;
using SQLite;

namespace Testapplicatie
{
	[Table("districts")]
	public class District
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string name { get; set; }
	}
}
