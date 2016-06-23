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
            CsvParser bikeTheftParser = new CsvParser("BikeThefts", 1);
            List<BikeTheft> bikeThefts = bikeTheftParser.ParseBikeTheft();

            CsvParser bikeContainerParser = new CsvParser("BikeContainers", 1);
            List<BikeContainer> bikeContainers = bikeTheftParser.ParseBikeContainer();

            Console.Write("");

            // Create SQLite file
            SQLiteConnection.CreateFile("Database.sqlite");

            // Connect
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=Database.sqlite;Version=3;");
            m_dbConnection.Open();

            //string sql = "CREATE TABLE highscores (name VARCHAR(20), score INT)";
            //SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            //command.ExecuteNonQuery();

            //sql = "insert into highscores (name, score) values ('Me', 3000)";
            //command = new SQLiteCommand(sql, m_dbConnection);
            //command.ExecuteNonQuery();
            //sql = "insert into highscores (name, score) values ('Myself', 6000)";
            //command = new SQLiteCommand(sql, m_dbConnection);
            //command.ExecuteNonQuery();
            //sql = "insert into highscores (name, score) values ('And I', 9001)";
            //command = new SQLiteCommand(sql, m_dbConnection);
            //command.ExecuteNonQuery();

            var sql = "select * from highscores order by score desc";
            var command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
            }
        }
    }
}
