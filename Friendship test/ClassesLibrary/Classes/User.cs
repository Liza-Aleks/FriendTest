using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClassesLibrary.Classes
{
    public class User
    {
        [JsonProperty("uid")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("photo_100")]
        public string PhotoUrl { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("online")]
        public int Online { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
    }
}
