using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportBicycleInfo
{
    class BikeContainer
    {
        public int ID;
        public double Lat;
        public double Long;

        public BikeContainer(double latCoord, double longCoord)
        {
            this.Lat = latCoord;
            this.Long = longCoord;
        }

        public static bool ValidCSVRow(string[] row)
        {
            return false;
        }

        public static BikeContainer ParseCSVRow(string[] row)
        {
            return new BikeContainer(.0f, .0f);
        }
    }
}
