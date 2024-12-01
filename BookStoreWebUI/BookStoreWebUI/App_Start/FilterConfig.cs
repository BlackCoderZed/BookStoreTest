using BookStoreWebUI.Utils;
using System.Web;
using System.Web.Mvc;

namespace BookStoreWebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomActionFilter());
        }
    }
}
