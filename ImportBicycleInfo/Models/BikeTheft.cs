using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportBicycleInfo
{
    class BikeTheft : Insertable
    {
        public string ID;
        public string Color;
        public string Brand;
        public DateTime DateTime;
        public string District;
        public string Street;

        public BikeTheft(string ID, string district, string street, string brand, string color, DateTime dateTime)
        {
            this.ID = ID;
            this.DateTime = dateTime;
            this.Brand = brand;
            this.Color = color;
            this.District = district;
            this.Street = street;
            
        }

        public static bool ValidCSVRow(string[] row)
        {
            string ID = row[0];
            string district = row[8];
            string color = row[24];
            string brand = row[22];
            string dateTime = row[11];
            string street = row[9];

            if (dateTime == "" || ID == "")
            {
                return false;
            }

            return true;
        }

        public static BikeTheft ParseCSVRow(string[] row)
        {
            string ID = row[0];
            string district = row[8];
            string color = row[24];
            string brand = row[22];
            string street = row[9];
            DateTime dateTime = DateTime.ParseExact(row[11], "dd-MM-yy", CultureInfo.InvariantCulture);

            return new BikeTheft(ID, district, street, brand, color, dateTime);
        }

        public override bool InsertDB(SQLiteConnection connection)
        {
            this.Connection = connection;
            // TODO: Implement create or get
            int brandID = this.GetOrSet("brands", this.Brand);
            int colorID = this.GetOrSet("colors", this.Color);
            int streetID = this.GetOrSet("streets", this.Street, "districts", "district_id", this.District);

            string query = "INSERT INTO `bikethefts` VALUES(@id, @brand_id, @color_id, @street_id);";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                // Apply values
                command.Parameters.AddWithValue("@id", this.ID);
                command.Parameters.AddWithValue("@brand_id", brandID);
                command.Parameters.AddWithValue("@color_id", colorID);
                command.Parameters.AddWithValue("@street_id", streetID);
                command.ExecuteNonQuery();
            }

            return true;
        }
    }
}
