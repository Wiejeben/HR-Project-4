using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImportBicycleInfo
{
    class GoogleGeocodeAPI
    {
        private string Key = "AIzaSyA_ZPBPlNBs2gmuFpEMfLZHIx5eu3ee9ng";
        public readonly JObject Result;

        public GoogleGeocodeAPI(string searchQuery)
        {
            WebRequest request = WebRequest.Create("https://maps.googleapis.com/maps/api/geocode/json?address="+ searchQuery +"&key=" + this.Key);
            WebResponse response = request.GetResponse();
           
            using (var responseStream = new StreamReader(response.GetResponseStream()))
            {
                string result = responseStream.ReadToEnd();
                this.Result = JObject.Parse(result);
            }
        }
    }
}
