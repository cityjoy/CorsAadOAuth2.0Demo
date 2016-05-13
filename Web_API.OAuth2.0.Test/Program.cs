using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace Web_API.OAuth2._0.Test
{
    public class Program
    {
        static void Main(string[] args)
        {

            #region 客户端请求
            HttpClient _httpClient;
            _httpClient = new HttpClient();
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
            Console.WriteLine(Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret)));
            #endregion
            parameters.Add("grant_type", "client_credentials");

            Token token = null;
            var response = _httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
            var responseValue = response.Result.Content.ReadAsStringAsync();
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                token = JsonConvert.DeserializeObject<Token>(responseValue.Result.ToString());
            }
            Console.WriteLine(responseValue.Result);
            Console.WriteLine("token:" + token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            Console.WriteLine((_httpClient.GetAsync("/api/values")).Result.Content.ReadAsStringAsync().Result);
            #endregion

            Console.Read();

        }

    }
    public class Token
    {
        [JsonProperty("Access_Token")]
        public string AccessToken { get; set; }
    }




}
