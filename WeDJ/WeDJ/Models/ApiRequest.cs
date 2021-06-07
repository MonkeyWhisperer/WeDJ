using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeDJ.Models
{
    public class ApiRequest
    {
        [JsonProperty(PropertyName = "application_code")]
        public string ApplicationCode { get; set; }

        [JsonProperty(PropertyName = "session_token")]
        public string SessionToken { get; set; }

        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

    }
}
