using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeDJ.Models;

namespace WeDJ.RestClient
{
    /// <summary>
    /// RestClient implements methods for calling CRUD operations
    /// using HTTP.
    /// </summary>
    public class RestClient<T>
    {
        private const string WebServiceUrl = WeDJ.Helpers.Settings.ServiceURL;

        public async Task<object> PostAsync(ApiRequest apiRequest, string methodName)
        {
            var httpClient = new HttpClient();

            string json = JsonConvert.SerializeObject(apiRequest, Formatting.Indented);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(WebServiceUrl + methodName, httpContent);

            var responseString = result.Content.ReadAsStringAsync().Result;

            var apiResponse = JsonConvert.DeserializeObject(responseString, typeof(ApiResponse), settings: null) as ApiResponse;

            return apiResponse.Response;
        }
    }

    public class RestClientWithError<T>
    {
        private const string WebServiceUrl = WeDJ.Helpers.Settings.ServiceURL;

        public async Task<ApiResponse> PostAsync(ApiRequest apiRequest, string methodName)
        {
            var httpClient = new HttpClient();

            string json = JsonConvert.SerializeObject(apiRequest, Formatting.Indented);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(WebServiceUrl + methodName, httpContent);

            var responseString = result.Content.ReadAsStringAsync().Result;

            var apiResponse = JsonConvert.DeserializeObject(responseString, typeof(ApiResponse), settings: null) as ApiResponse;

            return apiResponse;
        }
    }
}
