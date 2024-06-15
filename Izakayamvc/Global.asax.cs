using AutoMapper;
using Izakayamvc.ViewModels.Mapping;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Izakayamvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static class AutoMapperHelper
        {
            public static Mapper MapperObj { get; set; }
        }
        protected void Application_Start()
        {
            // 這裡要加上這行, 才會讓 AutoMapper 能夠找到 MappingProfile 類別
            var config = new MapperConfiguration(cfg =>
            {
                // cfg.CreateMap<Category, CategoryDto>(); //如果需要一個個對應, 就這樣寫
                cfg.AddProfile<MappingProfile>();
            });
            AutoMapperHelper.MapperObj = new Mapper(config);



            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //404顯示頁面
        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            var httpEx = ex as HttpException;
            if (httpEx != null)
            {
                int statusCode = httpEx.GetHttpCode();
                switch (statusCode)
                {
                    case 404:
                        Server.TransferRequest("/ErrorHandler/NotFound");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
