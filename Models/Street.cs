using System;
using SQLite;

namespace Testapplicatie
{
	[Table("streets")]
	public class Street
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string name { get; set; }
		public int drums { get; set; }
	}
}
