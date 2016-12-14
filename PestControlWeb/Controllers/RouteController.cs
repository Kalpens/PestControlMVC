using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PestControlDll;
using PestControlDll.Entities;
using PestControlDll.Interfaces;
using System.Net.Http;
using System.Net;

namespace PestControlWeb.Controllers
{
    public class RouteController : Controller
    {
        private IServiceGateway<Destination> dm = new DllFacade().GetDestinationServiceGateway();
        private IServiceGateway<Route> rm = new DllFacade().GetRouteServiceGateway();
        private IAccountGateway accountGateway = new DllFacade().GetAccountGateway();
        // GET: Route
        public ActionResult Index()
        {
            try
            {
                //ViewBag. <--- This has no reference, but i need something like Viewbag.Route = List<Routes> 
                //You did not make a refference to dllfacade to get service gateway, I just added it, line 13.
                //I do not know which one you wanted to do, but I did both ways, one passing into view, other one adding to the viewBag, but viewbag only had id and name.
                ViewBag.RoutId = new SelectList(rm.Get(), "Id", "Name");
                //return View(rm.Get());
                var currentUser = accountGateway.GetCurrentUser();
                return View(currentUser.Routes);
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("401"))
                    return RedirectToAction("Login", "Account", new { returnUrl = Request.Url.LocalPath });

                ViewBag.Error = ex.Message;
                return View("Error");
            }
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

        public ActionResult EditDestinations(int? routeId)
        {
            if (routeId != null)
            {
                return View(rm.Get(routeId.Value));
            }
            else
            {
                return View();
            }
        }

        public ActionResult EditRoute()
        {
            var user = accountGateway.GetCurrentUser();
            return View(user.Routes);
        }


        public ActionResult DisplayDestinations(int? routeId)
        {
            if (routeId != null)
            {
                return View(rm.Get(routeId.Value));
            }
            else
            {
                return View();
            }

        }

        // GET: Route/Delete/5
        [HttpGet]
        public ActionResult ConfirmDeleteDestination(int id)
        {
            var destinationToDelete = dm.Get(id);
            if (destinationToDelete == null)
            {
                return RedirectToAction("EditDestinations", new { routeId = destinationToDelete.RouteId });
            }

            return View(destinationToDelete);
        }

        [HttpPost]
        public ActionResult DeleteDestination(int? id, int routeId)
        {
            if(id.HasValue)
            {
                dm.Delete(id.Value);
                if (rm.Get(routeId).Destinations.Count == 0)
                {
                    rm.Delete(routeId);
                    return RedirectToAction("Index");
                }
                
            }
            return RedirectToAction("EditDestinations", new {routeId = routeId });
        }

        // GET: Route/Delete/5
        [HttpGet]
        public ActionResult ConfirmDeleteRoute(int? routeId)
        {
            var routeToDelete = rm.Get(routeId.Value);
            if (routeToDelete == null)
            {
                return RedirectToAction("Index");
            }

            return View(routeToDelete);
        }

        [HttpPost]
        public ActionResult DeleteRoute(int? routeId)
        {
            if (routeId.HasValue)
            {
                rm.Delete(routeId.Value);
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
    }

}