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
            string district = row[6];
            string color = row[24];
            string brand = row[22];
            string dateTime = row[11];
            string street = row[9];
            string type = row[21];

            // Validation
            return !(dateTime == "" ||
                ID == "" ||
                color == "0" ||
                color == "#N/A" ||
                color == "ONBEKEND" ||
                color == "" ||
                brand == "" ||
                brand == "-" ||
                brand == "#N/A" ||
                type == "FIETS"
            );
        }

        public static BikeTheft ParseCSVRow(string[] row)
        {
            TextInfo format = new CultureInfo("nl-NL", false).TextInfo;
            string ID = row[0];
            string district = format.ToTitleCase(row[6].ToLower().Split('/').First());
            string color = format.ToTitleCase(row[24].ToLower());
            string brand = format.ToTitleCase(row[22].ToLower());
            string street = format.ToTitleCase(row[9].ToLower());
            DateTime dateTime = DateTime.ParseExact(row[11], "dd-MM-yy", CultureInfo.InvariantCulture);

            return new BikeTheft(ID, district, street, brand, color, dateTime);
        }

        public override bool InsertDB(SQLiteConnection connection)
        {
            this.Connection = connection;

            int brandID = this.GetOrSet("brands", this.Brand);
            int colorID = this.GetOrSet("colors", this.Color);
            int streetID = this.GetOrSet("streets", this.Street, "districts", "district_id", this.District);

            string query = "INSERT INTO `bikethefts` VALUES(@id, @date, @brand_id, @color_id, @street_id);";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                // Apply values
                command.Parameters.AddWithValue("@id", this.ID);
                command.Parameters.AddWithValue("@date", this.ConvertToTimestamp(this.DateTime));
                command.Parameters.AddWithValue("@brand_id", brandID);
                command.Parameters.AddWithValue("@color_id", colorID);
                command.Parameters.AddWithValue("@street_id", streetID);
                command.ExecuteNonQuery();
            }

            return true;
        }
    }
}
