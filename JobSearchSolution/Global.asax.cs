using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JobSearchSolution
{
	public static class SessionValues
	{

		public static int CurrentUserId
		{
			get
			{
				return 6; // Just for now. 
				// return (int)System.Web.HttpContext.Current.Session["CurrentUserId"];
			}
			set
			{
				System.Web.HttpContext.Current.Session["CurrentUserId"] = value;
			}
		}
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
