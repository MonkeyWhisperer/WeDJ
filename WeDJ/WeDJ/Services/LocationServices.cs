using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDJ.Helpers;
using WeDJ.Models;
using WeDJ.RestClient;

namespace WeDJ.Services
{
    class LocationServices
    {

        public async Task<object> LoadLocations(RoomGetList room)
        {
            var restClient = new RestClient<string>();

            var apiRequest = new ApiRequest()
            {
                ApplicationCode = Settings.ApplicationCode,
                SessionToken = Settings.SessionToken,
                Data = room
            };

            var roomList = await restClient.PostAsync(apiRequest, ApiUrls.RoomGetList);
            
            return roomList;
        }


    }
}
