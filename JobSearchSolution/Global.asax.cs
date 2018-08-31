using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;

namespace JobSearchSolution
{
	public static class SessionValues
	{

		//public static Guid CurrentUserId_NO
		//{
		//	get
		//	{
		//		return new Guid(HttpContext.User.Identity.GetUserId());
		//	}
		//}
	}

	public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
