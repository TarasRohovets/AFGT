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
                new SelectListItem { Value = "2", Text = "Estilo Musical" },
                new SelectListItem { Value = "3", Text = "Data" },
                new SelectListItem { Value = "4", Text = "Cidade" }
            };

        public ActionResult Index()
        {
            var result = db.Eventos.OrderBy(evento => evento.Data).ToList();
            ViewBag.ListaPesquisa = list;
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(string ConteudoPesquisa, int? GeneroMusicalID, string ListaPesquisa, string Dia, string PointA, string TipoOpcao)
        {
            List<Evento> Evento = new List<Evento>();

            var evento = db.Eventos.OrderBy(e => e.Data);
            var result = evento.ToList();

            ViewBag.ListaPesquisa = list;

            switch (ListaPesquisa)
            {
<<<<<<< HEAD
                case "Data":

                    while (Dia == null)
=======
                case "1":

                    if (ConteudoPesquisa == "")
>>>>>>> BranchAline
                    {
                        result = evento.ToList();
                    }
                    if (TipoOpcao == "Local")
                    {
                        evento = db.Eventos.OrderBy(e => e.Morada.Cidade);
                        result = evento.Include(e => e.Artistas).Where(e => e.Artistas.Any(a => a.Nome.ToLower() == ConteudoPesquisa.ToLower())).ToList();
                    }
                    else
                    {
                        result = evento.Include(e => e.Artistas).Where(e => e.Artistas.Any(a => a.Nome.ToLower() == ConteudoPesquisa.ToLower())).ToList();
                    }

                    break;

                case "2":

                    if (GeneroMusicalID == null)
                    {
                        result = evento.ToList();
                    }
                    if (TipoOpcao == "Local")
                    {
                        evento = db.Eventos.OrderBy(e => e.Morada.Cidade);
                        result = evento.Include(e => e.Artistas).Where(e => e.Artistas.Any(a => a.GeneroMusicalID == GeneroMusicalID)).ToList();
                    }
                    else
                    {
                        result = evento.Include(e => e.Artistas).Where(e => e.Artistas.Any(a => a.GeneroMusicalID == GeneroMusicalID)).ToList();
                    }
                    break;

                case "3":
                    if (Dia == "")
                    {
                        result = evento.ToList();
                    }
                    else
                    {
                        result = evento.ToList().Where(model => model.Data.Value.ToString("yyyy-MM-dd") == Dia).ToList();
                    }
                    break;

                case "4":
                    evento = db.Eventos.OrderBy(e => e.Morada.Cidade);
                    if (PointA == "")
                    {
                        
                        result = evento.ToList();
                    }
                    else
                    {
                        result = evento.ToList().Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower()).ToList();
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