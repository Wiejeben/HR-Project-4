using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportBicycleInfo
{
    class BikeContainer
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
            string ID = row[0];
            Double latCoord = Convert.ToDouble(row[18]);
            Double longCoord = Convert.ToDouble(row[19]);
            string district = row[28];
            string street = row[9];

            return new BikeContainer(ID, district, street, latCoord, longCoord);
        }
    }
}
