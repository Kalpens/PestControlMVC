using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PestControlDll;
using PestControlWeb.Models;
using PestControlDll.Interfaces;

namespace PestControlWeb.Controllers
{
    public class AccountController : Controller
    {
        private IAccountGateway accountGateway = new DllFacade().GetAccountGateway();

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = accountGateway.Login(model.UserName, model.Password);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Invalid login attempt!");
            }

            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = accountGateway.Register(model.Email, model.Password,
                        model.ConfirmPassword);

                    if (response.IsSuccessStatusCode)
                    {
                        accountGateway.Login(model.Email, model.Password);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("", response.Content.ReadAsStringAsync().Result);
                }
            }

            return View(model);
            
        }
        // GET: Account/AccountInfo
        public ActionResult AccountInfo()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            System.Web.HttpContext.Current.Session["token"] = null;
            return RedirectToAction("Login");
        }
    }

}