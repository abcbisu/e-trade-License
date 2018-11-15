using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace etrade_server.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult u()
        {
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
