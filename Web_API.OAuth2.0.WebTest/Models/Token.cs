using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Web_API.OAuth2._0.WebTest.Models
{
    public class Token
    {
        [JsonProperty("Access_Token")]
        public string AccessToken { get; set; }

        public static string GetAccessToken()
        {
            HttpClient _httpClient;
            _httpClient = new HttpClient();
            string accessToken = "";
            Cache cache = HttpContext.Current.Cache;
            var tokenCache = cache.Get("Access_Token");
            if (tokenCache != null)
            {
                accessToken = tokenCache.ToString();
            }
            else
            {
                _httpClient.BaseAddress = new Uri("http://api.test.com");
                var parameters = new Dictionary<string, string>();
                //parameters.Add("client_id", "user007");
                //parameters.Add("client_secret", "007go");

                #region 使用Basic认证(推荐)

                var clientId = "user007";
                var clientSecret = "007go";
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret)));

                #endregion 使用Basic认证(推荐)

                parameters.Add("grant_type", "client_credentials");

                Token token = null;
                var response = _httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
                var responseValue = response.Result.Content.ReadAsStringAsync();
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    token = JsonConvert.DeserializeObject<Token>(responseValue.Result.ToString());
                }
                accessToken = token.AccessToken;
                cache.Insert("Access_Token", token.AccessToken, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
            }

            return accessToken;
        }
    }
}