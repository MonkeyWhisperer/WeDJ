using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeDJ.Models
{
    public class ApiResponse
    {
        [JsonProperty(PropertyName = "errors")]
        public List<ApiError> Errors { get; set; }

        [JsonProperty(PropertyName = "has_error")]
        public bool HasError { get; set; }

        [JsonProperty(PropertyName = "response")]
        public object Response { get; set; }

    }
}
