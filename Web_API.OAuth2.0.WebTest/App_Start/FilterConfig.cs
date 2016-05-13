using System.Web;
using System.Web.Mvc;

namespace Web_API.OAuth2._0.WebTest
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}