using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PestControlDll.Entities;

namespace PestControlWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(Route route)
        {
            ViewBag.DestinationList = new List<string>();
            return View(route);
        }
        [HttpPost]
        public ActionResult GenerateMap()
        {
            return RedirectToAction("Index");
        }
    }
}   