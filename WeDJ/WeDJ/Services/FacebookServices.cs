using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeDJ.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeDJ.RestClient;
using WeDJ.Helpers;

namespace WeDJ.Services
{
    public class FacebookServices
    {

        public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/v2.10/me/?fields=name,picture,work,website,religion,location,locale,link,cover,age_range,birthday,devices,email,first_name,last_name,gender,hometown,is_verified,languages&scope=email&access_token="
                + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

            return facebookProfile;
        }


        public async Task<object> FacebookLogin(FacebookLogin fbLogin)
        {
            var restClient = new RestClient<string>();

            var apiRequest = new ApiRequest()
            {
                ApplicationCode = Settings.ApplicationCode,
                SessionToken = null,
                Data = fbLogin
            };

            var roomList = await restClient.PostAsync(apiRequest, ApiUrls.FacebookLogin);

            return roomList;
        }

        public async Task<object> FacebookRegister(FacebookRegister fbRegister)
        {
            var restClient = new RestClient<string>();

            var apiRequest = new ApiRequest()
            {
                ApplicationCode = Settings.ApplicationCode,
                SessionToken = null,
                Data = fbRegister
            };

            var roomList = await restClient.PostAsync(apiRequest, ApiUrls.FacebookRegister);

            return roomList;
        }
    }
}
