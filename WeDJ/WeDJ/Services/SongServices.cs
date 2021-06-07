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
    class SongServices
    {
        public async Task<object> AddSongToParty(Song song)
        {
            var restClient = new RestClient<string>();

            var apiRequest = new ApiRequest()
            {
                ApplicationCode = Settings.ApplicationCode,
                SessionToken = Settings.SessionToken,
                Data = song
            };

            var zaSong = await restClient.PostAsync(apiRequest, ApiUrls.SongAddOne);

            return zaSong;
        }

        public async Task<object> RateSong(Song song)
        {
            var restClient = new RestClient<string>();

            var apiRequest = new ApiRequest()
            {
                ApplicationCode = Settings.ApplicationCode,
                SessionToken = Settings.SessionToken,
                Data = song
            };

            var zaSong = await restClient.PostAsync(apiRequest, ApiUrls.SongRate);

            return zaSong;
        }

        public async Task<object> UpdateSong(Song song)
        {
            var restClient = new RestClient<string>();

            var apiRequest = new ApiRequest()
            {
                ApplicationCode = Settings.ApplicationCode,
                SessionToken = Settings.SessionToken,
                Data = song
            };

            var zaSong = await restClient.PostAsync(apiRequest, ApiUrls.SongUpdate);

            return zaSong;
        }


        public async Task<object> LoadPartySongs(Party party)
        {
            var restClient = new RestClient<string>();

            var apiRequest = new ApiRequest()
            {
                ApplicationCode = Settings.ApplicationCode,
                SessionToken = Settings.SessionToken,
                Data = party
            };

            var songs = await restClient.PostAsync(apiRequest, ApiUrls.SongGetListByPartyId);

            return songs;
        }
    }
}
