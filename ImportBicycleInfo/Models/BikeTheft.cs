using Newtonsoft.Json.Linq;
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
        private string District;
        private string street;

        public string Street
        {
            get
            {
                return street;
            }

            set
            {
                JObject json = new GoogleGeocodeAPI(value + ", Rotterdam, The Netherlands").Result;
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

        public BikeTheft(string ID, string street, string brand, string color, DateTime dateTime)
        {
            this.ID = ID;
            this.DateTime = dateTime;
            this.Brand = brand;
            this.Color = color;
            this.Street = street;  
        }

        public static bool ValidCSVRow(string[] row)
        {
            string ID = row[0];
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
            string color = format.ToTitleCase(row[24].ToLower());
            string brand = format.ToTitleCase(row[22].ToLower());
            string street = format.ToTitleCase(row[9].ToLower());
            DateTime dateTime = DateTime.ParseExact(row[11], "dd-MM-yy", CultureInfo.InvariantCulture);

            return new BikeTheft(ID, street, brand, color, dateTime);
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
