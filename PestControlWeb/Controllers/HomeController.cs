using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PestControlDll;
using PestControlDll.Entities;
using PestControlWeb.Models;

namespace PestControlWeb.Controllers
{
    public class HomeController : Controller
    {
        private IServiceGateway<Destination> destinationGateway = new DllFacade().GetDestinationServiceGateway();
        private IServiceGateway<Route> routeGateway = new DllFacade().GetRouteServiceGateway();
        public ActionResult Index(Route route)
        {
            //If route temptada is not null, then the tempdata will be sent to the view
            if ((Route)TempData["route"] != null)
            {
                route = (Route)TempData["route"];
            }
            return View(route);
        }
        public ActionResult Map(Route route)
        {
            return View(route);
        }

        [HttpPost]
        public ActionResult AddAddressToRoute(MapViewModel model)
        {
            //User has logged in and all parameters are inserted
            if (model.Route.Id != 0 && System.Web.HttpContext.Current.Session["token"] != null && model.Address != null)
            {
                if (model.Route.Id == 0)
                {
                    model.Route.Date = DateTime.Now;
                    model.Route = routeGateway.Post(model.Route);
                }
                Destination destination = new Destination()
                { FullAddress = model.Address, RouteId = model.Route.Id};
                destinationGateway.Post(destination);
                TempData["route"] = model.Route;
                return RedirectToAction("Index");
            }
            //User has not logged in but has parameters
            else if (model.Route.Id != 0 && model.Address != null && System.Web.HttpContext.Current.Session["token"] == null)
            {
                model.Route.Destinations.Add(
                    new DllFacade().GetLocalDestinationManager().CreateDestination(model.Route, model.Address));
                TempData["route"] = model.Route;
                return RedirectToAction("Index");
            }
            //User has not logged in and has no route, but he has address
            else if (model.Route.Id == 0 && model.Address != null && System.Web.HttpContext.Current.Session["token"] == null)
            {
                var route = new Route()
                {
                    Date = DateTime.Now,
                    Name = "WillNotBeSaved",
                    Destinations = new List<Destination>(),
                    Id = -1
                };

                route.Destinations.Add(
                    new DllFacade().GetLocalDestinationManager().CreateDestination(model.Route, model.Address));
                TempData["route"] = route;
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}   