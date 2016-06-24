using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportBicycleInfo
{
    class BikeContainer : Insertable
    {
        public string ID;
        public double Lat;
        public double Long;
        public string District;
        public string Street;

        public BikeContainer(string ID, string district, string street, double latCoord, double longCoord)
        {
            this.ID = ID;
            this.District = district;
            this.Street = street;
            this.Lat = latCoord;
            this.Long = longCoord;
        }

        public static bool ValidCSVRow(string[] row)
        {
            string ID = row[0];
            string latCoord = row[18];
            string longCoord = row[19];
            string district = row[28];
            string street = row[9];

            if (ID == "" || latCoord == "" || longCoord == "")
            {
                return false;
            }

            return true;
        }

        public static BikeContainer ParseCSVRow(string[] row)
        {
            TextInfo format = new CultureInfo("nl-NL", false).TextInfo;
            string ID = row[0];
            Double latCoord = Convert.ToDouble(row[18]);
            Double longCoord = Convert.ToDouble(row[19]);
            string district = format.ToTitleCase(row[28].ToLower());
            string street = format.ToTitleCase(row[9].ToLower());

            return new BikeContainer(ID, district, street, latCoord, longCoord);
        }

        public override bool InsertDB(SQLiteConnection connection)
        {
            this.Connection = connection;

            int streetID = this.GetOrSet("streets", this.Street, "districts", "district_id", this.District);

            string query = "INSERT INTO `bikethefts` VALUES(@id, @lat, @long, @street_id);";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                // Apply values
                command.Parameters.AddWithValue("@id", this.ID);
                command.Parameters.AddWithValue("@lat", this.Lat);
                command.Parameters.AddWithValue("@long", this.Long);
                command.Parameters.AddWithValue("@street_id", streetID);
                command.ExecuteNonQuery();
            }

            return true;
        }
    }
}
