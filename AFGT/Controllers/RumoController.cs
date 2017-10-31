using AFGT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace AFGT.Controllers
{
    public class RumoController : Controller
    {
        // GET: Rumo
        private afgtEntities db = new afgtEntities();

        public ActionResult Index()
        {
            //Evento evento = db.Eventos.Find(id);
            string url1 = "https://www.google.com/maps/embed/v1/directions?key=";
            string key = WebConfigurationManager.AppSettings["GoogleMapsAPIKey"];
            string PointA = Request.Form["PointA"].ToString();
            string PointB = "Lisboa";//evento.Morada.ToString();

            if (PointA != null)
            {
                ViewBag.Link = url1 + key + "&origin=" + PointA + "&destination=" + PointB;
            }
            else
            {
                ViewBag.Link = "https://www.google.com/maps/embed/v1/place?key=" + key + " &q=" + PointB;
            }

            return View();
        }

    }
}
