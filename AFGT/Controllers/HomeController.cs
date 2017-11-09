using AFGT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Data.Entity;
using System.Globalization;

namespace AFGT.Controllers
{
    public class HomeController : Controller
    {
        private afgtEntities db = new afgtEntities();

        List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Artista" },
                new SelectListItem { Value = "2", Text = "Estilo Musical" }
            };

        public ActionResult Index()
        {
            var result = db.Eventos.OrderBy(evento => evento.Data).ToList();
            ViewBag.ListaPesquisa = list;
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(string ConteudoPesquisa, string GeneroMusicalID, string ListaPesquisa, string Dia, string PointA, string TipoOpcao)
        {
            List<Evento> Evento = new List<Evento>();

            var evento = db.Eventos.OrderBy(e => e.Data);
            var result = evento.ToList();


            ViewBag.ListaPesquisa = list;

            TempData["PointA"] = PointA;

            switch (TipoOpcao)
            {
                case "Data":
   
                    while (Dia == null)
                    {
                        result = evento.ToList();
                    }
                    if (GeneroMusicalID == "" && ConteudoPesquisa == "")
                    {
                        result = evento.ToList().Where(model => model.Data.Value.ToString("yyyy-MM-dd") == Dia).ToList();
                    }
                    else if (!(GeneroMusicalID == "" && ConteudoPesquisa == "") && ListaPesquisa == "2")
                    {
                        result = evento.Include(c => c.Artistas.Select(a => a.GeneroMusicalID.ToString() == GeneroMusicalID)).ToList().Where(model => model.Data.Value.ToString("yyyy-MM-dd") == Dia).ToList();
                    }
                    else
                    {
                        result = evento.Include(c => c.Artistas.Select(a => a.Nome.ToString().ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == "")).ToList().Where(model => model.Data.Value.ToString("yyyy-MM-dd") == Dia).ToList();
                    }
                break;

                case "Local":
                    while (PointA == "")
                    {
                        result = evento.OrderBy(e => e.Morada.Cidade).ToList();
                    }
                    if (GeneroMusicalID == "" && ConteudoPesquisa == "")
                    {
                        result = evento.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == "").ToList();
                    }
                    else if (!(GeneroMusicalID == "" && ConteudoPesquisa == "") && ListaPesquisa == "2")
                    {
                        result = evento.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == "").Include(c => c.Artistas.Select(a => a.GeneroMusicalID.ToString() == GeneroMusicalID)).ToList();
                    }
                    else 
                    {
                        result = evento.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == "").Include(c => c.Artistas.Select(a => a.Nome.ToString().ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == "")).ToList();
                    }
                break;
            }

            return PartialView("_ResultadosPesquisa", result);
        }

        public ActionResult _Local()
        {
            return View();
        }

    }
}