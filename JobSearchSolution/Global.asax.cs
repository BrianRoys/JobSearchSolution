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
		public static Opp CurrentOpp
		{
			get
			{
				return (Opp)HttpContext.Current.Session["CurrentOpp"];

			}
			set
			{
				HttpContext.Current.Session["CurrentOpp"] = value;
			}
		}

		public static Contact CurrentContact
		{
			get
			{
				return (Contact)HttpContext.Current.Session["CurrentContact"];

			}
			set
			{
				HttpContext.Current.Session["CurrentContact"] = value;
			}
		}

		public static Event CurrentEvent
		{
			get
			{
				return (Event)HttpContext.Current.Session["CurrentEvent"];

			}
			set
			{
				HttpContext.Current.Session["CurrentEvent"] = value;
			}
		}

		public static List<Opp> SelectedOpps
		{
			get
			{
				return (List<Opp>)HttpContext.Current.Session["SelectedOpps"];

			}
			set
			{
				HttpContext.Current.Session["SelectedOpps"] = value;

			}
		}

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
