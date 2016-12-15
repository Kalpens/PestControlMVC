using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PestControlDll;
using PestControlDll.Entities;
using PestControlDll.Interfaces;
using PestControlWeb.Models;

namespace PestControlWeb.Controllers
{
    public class HomeController : Controller
    {
        private IServiceGateway<Destination> destinationGateway = new DllFacade().GetDestinationServiceGateway();
        private IServiceGateway<Route> routeGateway = new DllFacade().GetRouteServiceGateway();
        private IServiceGateway<Worksheet> worksheetGateway = new DllFacade().GetWorksheetServiceGateway();
        private IServiceGateway<PestType> pestTypeGateway = new DllFacade().GetPestTypeServiceGateway();
        private IAccountGateway accountGateway = new DllFacade().GetAccountGateway();

        public ActionResult Index(int? routeId)
        {
            try {
                if (routeId != null)
                {
                    var route = routeGateway.Get(routeId.Value);
                    return View(route);
                }
                else
                {
                    return View();
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

        public ActionResult Map(int? routeId)
        {
            if (routeId != null)
            {
                try {
                    var route = routeGateway.Get(routeId.Value);
                    return View(route);
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
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Worksheet(int? id)
        {
            if (id != null)
            {
                try {
                    var worksheet = worksheetGateway.Get(id.Value);
                    var model = new WorksheetViewModel();
                    model.Worksheet = worksheet;
                    model.PestTypes = pestTypeGateway.Get();
                    return View(model);
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
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAddressToRoute(MapViewModel model)
        {
            try
            {
                var currentUser = accountGateway.GetCurrentUser();
                //There is already a route
                if (model.Route.Id != 0 && model.Address != null)
                {
                    Destination destination = new Destination()
                    { FullAddress = model.Address, RouteId = model.Route.Id };
                    destinationGateway.Post(destination);
                    return RedirectToAction("Index", new {routeId = model.Route.Id});
                }
                //There isn't a route so it is created and first destination added.
                else if (model.Route.Id == 0 && model.Address != null)
                {
                    Route route = new Route()
                    {
                        Date = DateTime.Now,
                        Name = model.Route.Name,
                        UserId = currentUser.Id
                    };
                    route = routeGateway.Post(route);
                    destinationGateway.Post(new Destination()
                    { FullAddress = model.Address, RouteId = route.Id });
                    return RedirectToAction("Index", new { routeId = route.Id });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("401"))
                    return RedirectToAction("Login", "Account", new { returnUrl = Request.Url.Host });

                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }
    }
}   