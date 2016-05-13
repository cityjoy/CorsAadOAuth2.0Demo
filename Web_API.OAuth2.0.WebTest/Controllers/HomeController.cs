using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web_API.OAuth2._0.WebTest.Models;

namespace Web_API.OAuth2._0.WebTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string accessToken = Token.GetAccessToken();
            HttpClient _httpClient;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string result="";
            try
            {
                result = (_httpClient.GetAsync("http://api.test.com/api/values")).Result.Content.ReadAsStringAsync().Result;

            }
            catch (Exception)
            {
                
                throw;
            }
            ViewBag.Token = accessToken;
            ViewBag.Result = result;
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "你的应用程序说明页。";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "你的联系方式页。";

            return View();
        }
    }
   
}
