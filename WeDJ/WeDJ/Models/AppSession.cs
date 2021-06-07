using Newtonsoft.Json;


namespace WeDJ.Models
{

    public class AppSession  
    {
        private UserDetails _user = new UserDetails();     

        [JsonProperty(PropertyName = "session_token")]   
        public string SessionToken { get; set; }
        [JsonProperty(PropertyName = "two_step_code")]
        public string TwoStepCode { get; set; }
        [JsonProperty(PropertyName = "validation_code")]
        public string ValidationCode { get; set; }
        [JsonProperty(PropertyName = "user")]
        public UserDetails User { get { return _user; } set { _user = value; } }

 
    }
}
