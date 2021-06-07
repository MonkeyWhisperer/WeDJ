using Newtonsoft.Json;
using System;

namespace WeDJ.Models
{
    public class FacebookRegister
    {
        [JsonProperty(PropertyName = "facebook_user_id")]
        public string FacebookUserID { get; set; }
        [JsonProperty(PropertyName = "facebook_access_token")]
        public string FacebookAccessToken { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "profile_picture")]
        public string ProfilePicture { get; set; }
        [JsonProperty(PropertyName = "birth_date")]
        public DateTime? BirthDate { get; set; }

    }
}
