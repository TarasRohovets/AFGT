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
        string key = "AIzaSyDBY66jDfPCD2B_rIRaQnIJW_x2xC - i7Xc";//WebConfigurationManager.AppSettings["GoogleMapsAPIKey"];
 
        public ActionResult Index(int id)
        {
            Evento evento = db.Eventos.Find(id);
            string PointB = evento.Morada.Endereco+","+evento.Morada.CodPostal+ ","+evento.Morada.Cidade;

            ViewBag.Link = "https://www.google.com/maps/embed/v1/place?key=" + key + " &q=" + PointB;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string PointA, int id)
        {
            Evento evento = db.Eventos.Find(id);
            string PointB = evento.Morada.Endereco + "," + evento.Morada.CodPostal + "," + evento.Morada.Cidade;
            string url1 = "https://www.google.com/maps/embed/v1/directions?key=";

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
