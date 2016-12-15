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
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }
        
        public ActionResult EditDestinations(int? routeId)
        {
            try
            {
                if (routeId != null)
                {
                    return View(rm.Get(routeId.Value));
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("401"))
                    return RedirectToAction("Login", "Account", new { returnUrl = Request.Url.LocalPath });

                ViewBag.Error = ex.Message;
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        // GET: Route/Delete/5
        [HttpGet]
        public ActionResult ConfirmDeleteDestination(int id)
        {
            try {
                var destinationToDelete = dm.Get(id);
                if (destinationToDelete == null)
                {
                    return RedirectToAction("EditDestinations", new { routeId = destinationToDelete.RouteId });
                }

                return View(destinationToDelete);
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("401"))
                    return RedirectToAction("Login", "Account", new { returnUrl = Request.Url.LocalPath });

                ViewBag.Error = ex.Message;
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDestination(int? id, int routeId)
        {
            try {
                if (id.HasValue)
                {
                    dm.Delete(id.Value);
                    if (rm.Get(routeId).Destinations.Count == 0)
                    {
                        rm.Delete(routeId);
                        return RedirectToAction("Index");
                    }

                }
                return RedirectToAction("EditDestinations", new { routeId = routeId });
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("401"))
                    return RedirectToAction("Login", "Account", new { returnUrl = Request.Url.LocalPath });

                ViewBag.Error = ex.Message;
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        // GET: Route/Delete/5
        [HttpGet]
        public ActionResult ConfirmDeleteRoute(int? routeId)
        {
            try {
                var routeToDelete = rm.Get(routeId.Value);
                if (routeToDelete == null)
                {
                    return RedirectToAction("Index");
                }

                return View(routeToDelete);
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("401"))
                    return RedirectToAction("Login", "Account", new { returnUrl = Request.Url.LocalPath });

                ViewBag.Error = ex.Message;
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoute(int? routeId)
        {
            try {
                if (routeId.HasValue)
                {
                    rm.Delete(routeId.Value);
                    return RedirectToAction("Index");

                }
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("401"))
                    return RedirectToAction("Login", "Account", new { returnUrl = Request.Url.LocalPath });

                ViewBag.Error = ex.Message;
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }
    }

}