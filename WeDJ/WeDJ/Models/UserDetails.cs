using System;
using Newtonsoft.Json;
 

namespace WeDJ.Models
{
    public class UserDetails 
    {
        [JsonProperty(PropertyName = "user_id")]
        public int UserID { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "middle_name")]
        public string MiddleName { get; set; }
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "full_name")]
        public string FullName
        {
            get
            {
                return 
                    FirstName + " " +
                    (string.IsNullOrEmpty(MiddleName) ? "" : MiddleName + " ")+ 
                    LastName;
            }
        }
        [JsonProperty(PropertyName = "profile_picture")]
        public string ProfilePicture { get; set; }    
        [JsonProperty(PropertyName = "last_login")]
        public DateTime? LastLogin { get; set; }       
    }
}
