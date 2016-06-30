using System.IO;
using SQLite;
using Android.App;

namespace AndroidBicycleInfo
{
	public class Database
	{
		private static string FileName = "Database.sqlite";
		private static string DbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), FileName);

		public static SQLiteConnection Load()
		{
			return new SQLiteConnection(DbPath);
		}

		public static void Boot(Activity activity)
		{
			// Check if your DB has already been extracted.
			if (!File.Exists(DbPath))
			{
				Database.LoadDatabase(activity);
			}
			else
			{
				using (Stream stream = new FileStream(DbPath, FileMode.Open))
				{
					// Check if file has contents
					if (stream.Length < 10000)
					{
						Database.LoadDatabase(activity);
					}
				}
			}
		}

		private static void LoadDatabase(Activity activity)
		{
			using (BinaryReader br = new BinaryReader(activity.Assets.Open(FileName)))
			{
				using (BinaryWriter bw = new BinaryWriter(new FileStream(DbPath, FileMode.Create)))
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

