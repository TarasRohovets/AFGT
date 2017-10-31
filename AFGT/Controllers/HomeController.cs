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

        public ActionResult Index(string ConteudoPesquisa)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "1", Text = "Artista" });
            list.Add(new SelectListItem { Value = "2", Text = "Estilo Musical" });

            ViewBag.ListaPesquisa = list;

            if (ConteudoPesquisa == null)
            {
                return View();
            }
            else
            {
                ConteudoPesquisa = Request.Form["ConteudoPesquisa"].ToString();
                string TipoPesquisa = Request.Form["ListaPesquisa"].ToString();
                return View();
            }
        }

        [HttpPost, ActionName("Index")]
        public ActionResult Data(DateTime Dia, string TipoPesquisa, string ConteudoPesquisa)
        {
            List<Evento> Evento = new List<Evento>();

            while (ConteudoPesquisa == null)
            {
                return View(db.Eventos.Where(model => model.Data == Dia || Dia == null).ToList());
            }

            if (TipoPesquisa == "Estilo Musical")
            {
                return View(db.Eventos.Where(model => model.Data == Dia || Dia == null).ToList());//.Where(model => model.Artistas.GeneroMusical.NomeEstilo.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
            }
            else
            {
                return View(db.Eventos.Where(model => model.Data == Dia || Dia == null).ToList().Where(model => model.Artistas.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
            }           
        }

        [HttpPost, ActionName("Index")]
        public ActionResult Local(string PointA, string TipoPesquisa, string ConteudoPesquisa)
        {
            List<Evento> Evento = new List<Evento>();

            while (ConteudoPesquisa == null)
            {
                return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList());
            }

            if (TipoPesquisa == "Estilo Musical")
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