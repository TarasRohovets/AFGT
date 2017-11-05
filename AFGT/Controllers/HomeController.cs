using AFGT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

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
                        return View(db.Eventos.Where(model => model.Data.ToString() == Data || Data == null).ToList()); //.Where(model => model.Artistas1.GeneroMusicalID.NomeEstilo.ToLower() == GeneroMusicalID.ToLower() || GeneroMusicalID == null));
                    }
                    else
                    {
                        return View(db.Eventos.Where(model => model.Data.ToString() == Data || Data == null).ToList().Where(model => model.Artistas.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
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
                        return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList());//.Where(model => model.Artistas.GeneroMusicalID.NomeEstilo.ToLower() == GeneroMusicalID.ToLower() || GeneroMusicalID == null));
                    }
                    else
                    {
                        return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList().Where(model => model.Artistas.ToLower() == GeneroMusicalID.ToLower() || GeneroMusicalID == null));
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