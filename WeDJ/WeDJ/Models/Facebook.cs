using Newtonsoft.Json;

namespace WeDJ.Models

{
    public class FacebookAppToken
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
    }

    public class FacebookUserToken
    {
        [JsonProperty(PropertyName = "data")]
        public FacebookUserTokenData Data { get; set; }
    }

    public class FacebookUserTokenData
    {
        [JsonProperty(PropertyName = "app_id")]
        public string AppID { get; set; }
        [JsonProperty(PropertyName = "is_valid")]
        public bool IsValid { get; set; }
        [JsonProperty(PropertyName = "user_id")]
        public string UserID { get; set; }
    }
}
