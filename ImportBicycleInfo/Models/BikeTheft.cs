using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportBicycleInfo
{
    class BikeTheft
    {
        public string ID;
        public string Color;
        public string Brand;
        public DateTime DateTime;
        public string District;

        public BikeTheft(string ID, string district, string brand, string color, DateTime dateTime)
        {
            this.ID = ID;
            this.DateTime = dateTime;
            this.Brand = brand;
            this.Color = color;
            this.District = district;
            
        }

        public static bool ValidCSVRow(string[] row)
        {
            string ID = row[0];
            string district = row[8];
            string color = row[24];
            string brand = row[22];
            string dateTime = row[11];

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
            DateTime dateTime = DateTime.ParseExact(row[11], "dd-MM-yy", CultureInfo.InvariantCulture);

            return new BikeTheft(ID, district, brand, color, dateTime);
        }
    }
}
