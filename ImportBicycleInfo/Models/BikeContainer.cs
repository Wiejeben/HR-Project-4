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
        public double Lat;
        public double Long;
        public string District;
        private string street;

        public string Street
        {
            get
            {
                return street;
            }

            set
            {
                JObject json = new GoogleGeocodeAPI(value).Result;
                var results = json.SelectToken("results");
                var result = results.First()["address_components"];

                foreach (var item in result)
                {
                    string type = item.SelectToken("types").Last().Value<string>();

                    // Streetname
                    if (type == "route")
                    {
                        this.street = item.SelectToken("long_name").Value<string>();
                    }

                    // City area
                    if (type == "sublocality_level_1")
                    {
                        this.District = item.SelectToken("long_name").Value<string>();
                    }
                }
            }
        }

        public BikeContainer(string ID, string street, double latCoord, double longCoord)
        {
            this.ID = ID;
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

            return !(ID == "" || latCoord == "" || longCoord == "");
        }

        public static BikeContainer ParseCSVRow(string[] row)
        {
            TextInfo format = new CultureInfo("nl-NL", false).TextInfo;
            string ID = row[0];
            Double latCoord = double.Parse(row[18]);
            Double longCoord = double.Parse(row[19]);
            string street = format.ToTitleCase(row[9].ToLower());

            return new BikeContainer(ID, street, latCoord, longCoord);
        }

        public override bool InsertDB(SQLiteConnection connection)
        {
            this.Connection = connection;

            int streetID = this.GetOrSet("streets", this.Street, "districts", "district_id", this.District);

            string query = "INSERT INTO `bikecontainers` VALUES(@id, @lat, @long, @street_id);";
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
