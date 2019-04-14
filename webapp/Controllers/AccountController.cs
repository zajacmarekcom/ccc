using webapp.Models;
using webapp.Security;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace webapp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model, string returnUrl = "")
        {
            if (ModelState.IsValid && Membership.ValidateUser(model.UserName, model.Password))
            {
                var user = (CustomMembershipUser)(new CustomMembershipProvider().GetUser(model.UserName, false));
                CreateCookie(user, model.RememberMe);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Nieprawidłowy login lub hasło");
                return View(model);
            }

            //return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", null);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new CustomMembershipProvider().CreateUser(model);
                CreateCookie(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Nieprawidłowe dane");
                return View(model);
            }
        }

        public JsonResult ValidateUserName(string username)
        {
            string message;
            return Users.UserAlradyExists(username, out message) ? Json(message, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
        }

        public void CreateCookie(CustomMembershipUser user, bool remebmerMe)
        {
            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.UserId = user.UserId;
            serializeModel.UserName = user.UserName;
            serializeModel.FirstName = user.FirstName;
            serializeModel.LastName = user.LastName;

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                0,
                user.UserName,
                DateTime.Now,
                DateTime.Now.AddMinutes(3000),
                remebmerMe,
                userData,
                FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }

    }
}
