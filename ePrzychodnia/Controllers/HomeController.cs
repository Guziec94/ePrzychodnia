using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ePrzychodnia.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Informacje o naszym projekcie.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Strona kontaktowa.";

            return View();
        }
    }
}