using Newtonsoft.Json;
using System;

namespace WeDJ.Models

{
    public class FacebookLogin
    {
        [JsonProperty(PropertyName = "facebook_user_id")]
        public string FacebookUserID { get; set; }
        [JsonProperty(PropertyName = "facebook_access_token")]
        public string FacebookAccessToken { get; set; }
    }
}
