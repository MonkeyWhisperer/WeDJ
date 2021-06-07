using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeDJ.Models
{
    [JsonObject]
    public class ApiError
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "supported_version", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SupportedVersion { get; set; }

        public ApiError()
        {

        }


        public ApiError(string message, string title = "")
        {
            Title = string.IsNullOrEmpty(title) ? "Notification Message" : title;
            Message = message;
        }
    }
}
