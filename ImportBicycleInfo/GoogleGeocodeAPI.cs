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
    class GoogleLocation
    {
        public string Street;
        public string District;

        public bool Validate()
        {
            return !(this.Street == null && this.District == null);
        }
    }

    class GoogleGeocodeAPI
    {
        private string Key = "AIzaSyBxeDKK7tN0s3zP-xTKq4qDW_frSQPtneU";

        public JObject Result { private set; get; }

        private void Request(string query)
        {
            WebRequest request = WebRequest.Create("https://maps.googleapis.com/maps/api/geocode/json?address=" + query + "&key=" + this.Key);
            WebResponse response = request.GetResponse();

            // Read result and parse to JSON object
            using (var responseStream = new StreamReader(response.GetResponseStream()))
            {
                string result = responseStream.ReadToEnd();
                this.Result = JObject.Parse(result);
            }
        }

        public static GoogleLocation GetStreetAndDistrict(string street)
        {
            GoogleLocation location = new GoogleLocation();
            GoogleGeocodeAPI api = new GoogleGeocodeAPI();

            Console.WriteLine("Query Google: " + street);
            api.Request(street);

            try
            {
                JObject json = api.Result;
                JToken results = json.SelectToken("results").First().SelectToken("address_components");

                foreach (JToken result in results)
                {
                    foreach (JToken types in result.SelectToken("types"))
                    {
                        string type = types.Value<string>();

                        switch (type)
                        {
                            case "route":
                                location.Street = result.SelectToken("long_name").Value<string>();
                                break;

                            case "sublocality":
                                location.District = result.SelectToken("long_name").Value<string>();
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                
            }
            

            return location;
        }
    }

}
