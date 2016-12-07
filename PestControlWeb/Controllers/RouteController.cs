using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PestControlDll;
using PestControlDll.Entities;

namespace PestControlWeb.Controllers
{
    public class RouteController : Controller
    {
        private IServiceGateway<Route> rm = new DllFacade().GetRouteServiceGateway();
        // GET: Route
        public ActionResult Index()
        {
            //ViewBag. <--- This has no reference, but i need something like Viewbag.Route = List<Routes> 
            //You did not make a refference to dllfacade to get service gateway, I just added it, line 13.
            //I do not know which one you wanted to do, but I did both ways, one passing into view, other one adding to the viewBag, but viewbag only had id and name.
            ViewBag.RoutId = new SelectList(rm.Get(), "Id", "Name");
            return View(rm.Get());
        }

        // GET: Route/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Route/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Route/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Route/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Route/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Route/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Route/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
