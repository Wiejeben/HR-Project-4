using System.IO;
using SQLite;
using Android.App;

namespace AndroidBicycleInfo
{
	public class Database
	{
		private static string FileName = "Database.sqlite";
		private static string Path = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/" + FileName;

		public static SQLiteConnection Load()
		{
			return new SQLiteConnection(Path);
		}

		public static void Boot(Activity activity)
		{
			// Check if your DB has already been extracted.
			if (!File.Exists(Path))
			{
				using (BinaryReader br = new BinaryReader(activity.Assets.Open(FileName)))
				{
					using (BinaryWriter bw = new BinaryWriter(new FileStream(Path, FileMode.Create)))
					{
						byte[] buffer = new byte[2048];
						int len = 0;
						while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
						{
							bw.Write(buffer, 0, len);
						}
					}
				}
			}
		}
	}
}

