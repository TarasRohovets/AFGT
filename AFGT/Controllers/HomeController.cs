using AFGT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AFGT.Controllers
{
    public class HomeController : Controller
    {
        private afgtEntities db = new afgtEntities();
        public ActionResult Index()
        {
            ViewBag.Generos = new SelectList(db.GeneroMusicals.ToList(), "GeneroMusicalID", "NomeEstilo");
            return View();
        }

        public ActionResult Data()
        {
            List<Models.Evento> Evento = new List<Models.Evento>();

            string TipoPesquisa = ""; //HttpRequest.Form.Get("search"); //----Ver melhor como funciona 

            //caso request.form devolva o texto da option em vez do seu valor
            if (TipoPesquisa == "Genero")
            {
                TipoPesquisa = "GeneroMusical";
            }
            else
            {
                TipoPesquisa = "Artista";
            }

            string ConteudoPesquisa = "";//HttpRequest.Form.Get("conteudo"); //----Ver melhor como funciona


            //if (Evento.TipoPesquisa.Contains(ConteudoPesquisa))

            //List <Models.Evento> SortedEvent = Evento.TipoPesquisa.All("ConteudoPesquisa").Data.sort();

            return View((object)Evento);
        }

        public ActionResult Local()
        {
            List<Models.Evento> Evento = new List<Models.Evento>();
            return View((object)Evento);
        }
    }
}