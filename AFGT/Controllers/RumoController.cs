using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AFGT.Controllers
{
    public class RumoController : Controller
    {
        // GET: Rumo
        public ActionResult Index()
        {
            string url1 = "https://www.google.com/maps/embed/v1/directions?key=";
            string key = "AIzaSyDBY66jDfPCD2B_rIRaQnIJW_x2xC-i7Xc";
            string PointA = "Coimbra";
            string PointB = "Lisboa";

            ViewBag.Link = url1 + key + "&origin=" + PointA + "&destination=" + PointB + url2;

            return View();
        }

    }
}
