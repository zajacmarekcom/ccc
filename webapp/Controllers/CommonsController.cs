using System.Linq;
using System.Web.Mvc;
using webapp.Models;

namespace webapp.Controllers
{
    public class CommonsController : Controller
    {
        WebappContext context = new WebappContext();

        public JsonResult CheckCode(string code)
        {
            var data = context.PostCodes.FirstOrDefault(c => c.PostCodeNumber == code);
            if (data == null) return null;

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
