using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobSearchSolution.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "What is the Job Search Solution?";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "You may contact me.";

			return View();
		}
	}
}