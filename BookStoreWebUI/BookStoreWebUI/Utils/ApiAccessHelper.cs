using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BookStoreWebUI.Utils
{
    public static class ApiAccessHelper
    {
        private static Uri _ApiUrl = null;

        public static void Initialize()
        {
            string uri = ConfigurationManager.AppSettings["WebApiUrl"];
            _ApiUrl = new Uri(uri);
        }

        public static async Task<string> SendPostRequestAsync(string requestPath, object requestData, string token)
        {
            if (_ApiUrl == null) { Initialize(); }

            string uri = new Uri(_ApiUrl, requestPath).ToString();

            // json obj
            string responseContent = null;
            string jsonData = JsonConvert.SerializeObject(requestData);

            using (HttpClient client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(token))
                {
                    // Add the Authorization header
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, content);

                responseContent = await response.Content.ReadAsStringAsync();
            }

            return responseContent;
        }

        public static async Task<string> SendGetRequestAsync(string requestPath, string query, string token)
        {
            if (_ApiUrl == null) { Initialize(); }

            // json obj
            string responseContent = null;
            string url = new Uri(_ApiUrl, requestPath).ToString();
            string urlWithQuery = string.Format("{0}{1}", url, query);

            using (HttpClient client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(token))
                {
                    // Add the Authorization header
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                HttpResponseMessage response = await client.GetAsync(urlWithQuery);

                responseContent = await response.Content.ReadAsStringAsync();
            }

            return responseContent;
        }
    }
}