using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportBicycleInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create SQLite file
            Console.WriteLine("Init DB");
            SQLiteConnection.CreateFile("Database.sqlite");

            // Connect
            SQLiteConnection connection = new SQLiteConnection("Data Source=Database.sqlite;Version=3;");
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand(Resource.sql_init, connection))
            {
                command.ExecuteNonQuery();
            }

            
            CsvParser bikeTheftParser = new CsvParser("BikeThefts", 1);
            CsvParser bikeContainerParser = new CsvParser("BikeContainers", 1);

            List<Insertable> insertables = new List<Insertable>();
            insertables.AddRange(bikeTheftParser.ParseBikeTheft());
            insertables.AddRange(bikeContainerParser.ParseBikeContainer());

            insertables.ForEach(m => m.InsertDB(connection));
        }
    }
}
