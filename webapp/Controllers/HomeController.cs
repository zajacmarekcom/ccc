using webapp.Security;
using System.Web.Mvc;

namespace webapp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [Authorize]
        public ActionResult Index()
        {
            if(((CustomPrincipal)User).CustomIdentity.UserRoleName == "agent")
            {
                return RedirectToAction("Index", "Agent");
            }
            else if (((CustomPrincipal)User).CustomIdentity.UserRoleName == "manager")
            {
                return RedirectToAction("Index", "Manager");
            }
            else if (((CustomPrincipal)User).CustomIdentity.UserRoleName == "admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (((CustomPrincipal)User).CustomIdentity.UserRoleName == "unactive")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize]
        public ActionResult Map()
        {
            return View();
        }
    }
}
