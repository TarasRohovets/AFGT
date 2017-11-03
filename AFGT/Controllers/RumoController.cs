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
        string key = WebConfigurationManager.AppSettings["GoogleMapsAPIKey"];
        string PointB = "Lisboa";//evento.Morada.ToString()+","+evento.Morada.Cidade+","+evento.Morada.CodPostal;

        public ActionResult Index()
        {
            ViewBag.Link = "https://www.google.com/maps/embed/v1/place?key=" + key + " &q=" + PointB;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string PointA)
        {
            //Evento evento = db.Eventos.Find(id);
            string url1 = "https://www.google.com/maps/embed/v1/directions?key=";

           if (PointA != null)
           {
                ViewBag.Link = url1 + key + "&origin=" + PointA + "&destination=" + PointB;//+ "&travelMode=" + "BICYCLING"
           }
            else
            {
                ViewBag.Link = "https://www.google.com/maps/embed/v1/place?key=" + key + " &q=" + PointB;
            }

            return View();
        }

        //public ActionResult TesteRumo()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>
        //    {
        //        new SelectListItem { Value = "DRIVING", Text = "Carro" },
        //        new SelectListItem { Value = "BICYCLING", Text = "Bicicleta" },
        //        new SelectListItem { Value = "TRANSIT", Text = "Transporte Publico" },
        //        new SelectListItem { Value = "WALKING", Text = "A pe" }
        //    };

        //    ViewBag.TravelMode = list;
        //    ViewBag.Link = "https://www.google.com/maps/embed/v1/place?key=" + key + " &q=" + PointB;
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult TesteRumo(string PointA, string TravelMode)
        //{
        //    //Evento evento = db.Eventos.Find(id);

        //    List<SelectListItem> list = new List<SelectListItem>
        //    {
        //        new SelectListItem { Value = "DRIVING", Text = "Carro" },
        //        new SelectListItem { Value = "BICYCLING", Text = "Bicicleta" },
        //        new SelectListItem { Value = "TRANSIT", Text = "Transporte Publico" },
        //        new SelectListItem { Value = "WALKING", Text = "Caminhando" }
        //    };

        //    ViewBag.TravelMode = list;

        //    string url1 = "http://maps.google.com/maps/api/js?key=";

        //    if (PointA != null)
        //    {
        //        ViewBag.Link = url1 + key + "&origin=" + PointA + "&destination=" + PointB + "&travelMode=" + TravelMode;
               
        //    }
        //    else
        //    {
        //        ViewBag.Link = "https://www.google.com/maps/embed/v1/place?key=" + key + " &q=" + PointB;
                
        //    }
        //    return View();
        //}
    }
}
