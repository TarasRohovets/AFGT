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
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "1", Text = "Artista" });
            list.Add(new SelectListItem { Value = "2", Text = "Estilo Musical" });

            ViewBag.ListaPesquisa = list;

            return View();
        }

        public ActionResult Data(DateTime Dia, string TipoPesquisa, string ConteudoPesquisa)
        {
            List<Models.Evento> Evento = new List<Models.Evento>();

            TipoPesquisa = ""; 
            ConteudoPesquisa = "";

            if (TipoPesquisa == "Genero")
            {
                return View(db.Eventos.Where(model => model.Data == Dia || Dia == null).ToList());//.Where(model => model.Artistas.GeneroMusical.NomeEstilo.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
            }
            else
            {
                return View(db.Eventos.Where(model => model.Data == Dia || Dia == null).ToList().Where(model => model.Artistas.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
            }
        }

        public ActionResult Local(String PointA, string TipoPesquisa, string ConteudoPesquisa)
        {
            List<Evento> Evento = new List<Evento>();

            ConteudoPesquisa = "";
            TipoPesquisa = Request.Form["ListaPesquisa"].ToString();

            if (TipoPesquisa == "EstiloMusical")
            {
                return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList());//.Where(model => model.Artistas.GeneroMusical.NomeEstilo.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
            }
            else
            {
                return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList().Where(model => model.Artistas.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
            }

        }
    }
}