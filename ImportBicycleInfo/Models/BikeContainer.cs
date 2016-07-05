using Newtonsoft.Json.Linq;
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
        public DateTime DateTime;
        public double Lat;
        public double Long;
        public string District;
        private string Street;
        
        public BikeContainer(string ID, DateTime dateTime, string street, string district, double latCoord, double longCoord)
        {
            this.ID = ID;
            this.DateTime = dateTime;
            this.Street = street;
            this.District = district;
            this.Lat = latCoord;
            this.Long = longCoord;
        }

        public static bool ValidCSVRow(string[] row)
        {
            string ID = row[0];
            string date = row[32];
            string latCoord = row[18];
            string longCoord = row[19];
            string district = row[28];
            string street = row[9];
            string type = row[5];

            return !(ID == "" || latCoord == "" || longCoord == "" || type != "Fietstrommel");
        }

        public static BikeContainer ParseCSVRow(string[] row)
        {
            TextInfo format = new CultureInfo("nl-NL", false).TextInfo;
            string ID = row[0];
            DateTime dateTime = DateTime.ParseExact(row[32], "dd-MM-yyyy", CultureInfo.InvariantCulture);
            Double latCoord = double.Parse(row[18]);
            Double longCoord = double.Parse(row[19]);

            string street = format.ToTitleCase(row[9].ToLower());
            GoogleLocation location = GoogleGeocodeAPI.GetStreetAndDistrict(street + " Rotterdam");

            if (location.Validate())
            {
                return new BikeContainer(ID, dateTime, location.Street, location.District, latCoord, longCoord);
            }

            return null;
        }

        public override bool InsertDB(SQLiteConnection connection)
        {
            if (this.Street == null || this.District == null)
                return false;

            this.Connection = connection;

            int streetID = this.GetOrSet("streets", this.Street, "districts", "district_id", this.District);

            string query = "INSERT INTO `bikecontainers` VALUES(@id, @date, @lat, @long, @street_id);";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                // Apply values
                command.Parameters.AddWithValue("@id", this.ID);
                command.Parameters.AddWithValue("@date", this.ConvertToTimestamp(this.DateTime));
                command.Parameters.AddWithValue("@lat", this.Lat);
                command.Parameters.AddWithValue("@long", this.Long);
                command.Parameters.AddWithValue("@street_id", streetID);
                command.ExecuteNonQuery();
            }

            return true;
        }
    }
}
