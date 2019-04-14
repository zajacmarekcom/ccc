using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using webapp.Models;
using Newtonsoft.Json.Linq;

namespace webapp.Controllers
{
    public class DataController : Controller
    {
        readonly WebappContext _context = new WebappContext();

        public JsonResult Districts(int id)
        {
            var districts = _context.Districts.Where(d => d.ProvinceId == id);
            var model = districts.Select(d => new DistrictJson()
            {
                Id = d.Id,
                Name = d.Name
            });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public static void GetPosition(ref BusinessData data)
        {
            try
            {
                var url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + data.BuildingNumber + "," +
                      data.Street.Replace(' ', '+') + "," + data.City.Replace(' ', '+') + "," + data.PostCode;
                var json = (new WebClient()).DownloadString(url);
                var obj = JObject.Parse(json);
                if (obj["status"].Value<string>() != "OK") return;
                data.Lat = "" + obj["results"][0]["geometry"]["location"]["lat"];
                data.Lng = "" + obj["results"][0]["geometry"]["location"]["lng"];
            }
            catch (Exception)
            {
                data.Lat = null;
                data.Lng = null;
            }

        }
    }
}
