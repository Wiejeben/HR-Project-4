using System;
using System.IO;
using SQLite;

namespace AndroidBicycleInfo
{
	public class Database
	{
		public static SQLiteConnection init()
		{
			string fileName = "Database.sqlite";
			string path = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), fileName);

			return new SQLiteConnection(path);
		}
	}
}

