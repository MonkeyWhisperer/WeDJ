using WeDJ.Models;
using WeDJ.RestClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDJ.Helpers;
//using WeDJ.Views;
using WeDJ;

namespace WeDJ.Services
{
    class UserServices
    {
        public async Task<ApiResponse> Login(User user)
        {
            var restClient = new RestClientWithError<string>();

            var apiRequest = new ApiRequest()
            {
                ApplicationCode = Settings.ApplicationCode,
                SessionToken = null,
                Data = user
            };

            ApiResponse userList = await restClient.PostAsync(apiRequest, ApiUrls.UserLogin);
            
            if (userList.Response != null)
            {
                JObject data = userList.Response as JObject;               
                var x = data.Value<string>("session_token");
                Settings.SessionToken = data.Value<string>("session_token");
                Settings.UserID = data.Value<JObject>("user").Value<int>("user_id");
            }
           
            return userList;
        }

        public async Task<ApiResponse> CreateAccount(User user)
        {
            var restClient = new RestClientWithError<string>();

            var apiRequest = new ApiRequest()
            {
                ApplicationCode = Settings.ApplicationCode,
                SessionToken = null,
                Data = user
            };

            ApiResponse userList = await restClient.PostAsync(apiRequest, ApiUrls.UserRegister);

            if (userList.Response != null)
            {
                JObject data = userList.Response as JObject;
                WeDJ.Helpers.Settings.SessionToken = data.Value<string>("SessionToken");                
            }
            return userList;
        }


        public async Task<ApiResponse> ForgotPassword(User user)
        {
            var restClient = new RestClientWithError<string>();

            var apiRequest = new ApiRequest()
            {
                ApplicationCode = Settings.ApplicationCode,
                SessionToken = null,
                Data = user
            };

            ApiResponse userList = await restClient.PostAsync(apiRequest, ApiUrls.ForgotPassword);            
            return userList;
        }
    }
}
