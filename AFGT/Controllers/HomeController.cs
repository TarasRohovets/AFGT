using AFGT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Data.Entity;

namespace AFGT.Controllers
{
    public class HomeController : Controller
    {
        private afgtEntities db = new afgtEntities();

        public ActionResult Index()
        {
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Artista" },
                new SelectListItem { Value = "2", Text = "Estilo Musical" }
            };

            ViewBag.ListaPesquisa = list;

            return View();
        }

        [HttpPost]
        public ActionResult Index(string ConteudoPesquisa, string GeneroMusicalID, string ListaPesquisa, string Data, string PointA, string TipoOpcao)
        {
            List<Evento> Evento = new List<Evento>();
            Artista Artista = new Artista();

            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Artista" },
                new SelectListItem { Value = "2", Text = "Estilo Musical" }
            };

            ViewBag.ListaPesquisa = list;

            TempData["PointA"] = PointA;

            switch (TipoOpcao)
            {
                case "Data":
                    while (Data == null)
                    {
                        return View(db.Eventos.ToList());
                    }
                    if (ConteudoPesquisa == null)
                    {
                        return View(db.Eventos.Where(model => model.Data.ToString() == Data || Data == null).ToList());
                    }
                    else if (ListaPesquisa == "2")
                    {
                        return View(db.Eventos.Where(model => model.Data.ToString() == Data || Data == null).Include(c => c.Artistas1.Select(a => a.GeneroMusicalID.ToString() == GeneroMusicalID)).ToList());
                    }
                    else
                    {
                        return View(db.Eventos.Where(model => model.Data.ToString() == Data || Data == null).Where(model => model.Artistas.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null).ToList());
                    }

                case "Local":
                    while (PointA == null)
                    {
                        return View(db.Eventos.ToList());
                    }
                    if (GeneroMusicalID == null)
                    {
                        return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList());
                    }
                    else if (ListaPesquisa == "2")
                    {
                        return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).Include(c => c.Artistas1.Select(a => a.GeneroMusicalID.ToString() == GeneroMusicalID)).ToList());
                    }
                    else
                    {
                        return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).Where(model => model.Artistas.ToLower() == GeneroMusicalID.ToLower() || GeneroMusicalID == null).ToList());
                    }
            }
            return PartialView("ResultadosPesquisa");
        }

        public ActionResult Local()
        {
            return View();
        }

    }
}