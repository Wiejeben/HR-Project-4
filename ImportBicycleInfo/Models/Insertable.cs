using System;
using System.Data.SQLite;

namespace ImportBicycleInfo
{
    abstract class Insertable
    {
        protected SQLiteConnection Connection;

        protected int GetLastInsertedID(string table)
        {
            string select_query = "SELECT `seq` FROM `sqlite_sequence` WHERE `name` == @table;";
            SQLiteCommand command = new SQLiteCommand(select_query, this.Connection);
            command.Parameters.AddWithValue("@table", table);

            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();

            return reader.GetInt32(0);
        }

        protected int GetOrSet(string table, string name)
        {
            string select_query = "SELECT `id` FROM `" + table + "` WHERE `name` == @name;";
            using (SQLiteCommand command = new SQLiteCommand(select_query, this.Connection))
            {
                command.Parameters.AddWithValue("@name", name);

                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Get existing: " + name);
                    return reader.GetInt32(0);
                }
            }

            string insert_query = "INSERT INTO `" + table + "` VALUES(null, @name);";
            using (SQLiteCommand command = new SQLiteCommand(insert_query, this.Connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();

                Console.WriteLine("Create new: " + name);
                return this.GetLastInsertedID(table);
            }
        }

        protected int GetOrSet(string table, string name, string childTable, string childColumn, string childName)
        {
            int child_id = this.GetOrSet(childTable, childName);

            string select_query = "SELECT `id` FROM `" + table + "` WHERE `name` == @name AND `" + childColumn + "` == @child_id;";
            using (SQLiteCommand command = new SQLiteCommand(select_query, this.Connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@child_id", child_id);

                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Get existing: " + name + " - With child: " + childName);
                    return reader.GetInt32(0);
                }
            }

            string insert_query = "INSERT INTO `" + table + "` VALUES(null, @name, @child_id);";

            using (SQLiteCommand command = new SQLiteCommand(insert_query, this.Connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@child_id", child_id);
                command.ExecuteNonQuery();

                Console.WriteLine("Create new: " + name + " - With child: " + childName);
                return this.GetLastInsertedID(table);
            }
        }

        protected int ConvertToTimestamp(DateTime value)
        {
            // UNIX starting time
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            // Get amount of seconds from start UNIX timestamp
            return Convert.ToInt32(span.TotalSeconds);
        }

        public abstract bool InsertDB(SQLiteConnection connection);
    }
}