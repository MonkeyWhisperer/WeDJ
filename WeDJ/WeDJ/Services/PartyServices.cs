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
    class PartyServices
    {

        public async Task<object> LoadParties(object room)
        {
            var restClient = new RestClient<string>();

            var apiRequest = new ApiRequest()
            {
                ApplicationCode = Settings.ApplicationCode,
                SessionToken = Settings.SessionToken,
                Data = room
            };

            var roomList = await restClient.PostAsync(apiRequest, ApiUrls.PartyGetListByRoomId);

            return roomList;
        }
    }
}
