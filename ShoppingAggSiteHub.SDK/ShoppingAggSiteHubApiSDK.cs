using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ShoppingAggSiteHub.SDK
{
    public class ShoppingAggSiteHubApiSDK
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<T> GetAsync<T>(string path)
        {
            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (!response.IsSuccessStatusCode)
                throw await ThrowForUnsuccessfulResponse(response);
            return await ReturnSuccess<T>(response);
        }

        public static async Task<TResponse> PostAsync<TBody, TResponse>(TBody requestBody, string path)
        {
            string postContent = JsonConvert.SerializeObject(requestBody);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(postContent);
            ByteArrayContent byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(path, byteContent);
            if (!response.IsSuccessStatusCode)
                throw await ThrowForUnsuccessfulResponse(response);

            return await ReturnSuccess<TResponse>(response);
        }

        public static async Task<T> DeleteAsync<T>(string path)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(path);
            if (!response.IsSuccessStatusCode)
                throw await ThrowForUnsuccessfulResponse(response);

            return await ReturnSuccess<T>(response);
        }

        private static async Task<T> ReturnSuccess<T>(HttpResponseMessage response)
        {
            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }

        private static async Task<Exception> ThrowForUnsuccessfulResponse(HttpResponseMessage response)
        {
            string result = await response.Content.ReadAsStringAsync();
            return (new Exception(result));
        }
    }
}
