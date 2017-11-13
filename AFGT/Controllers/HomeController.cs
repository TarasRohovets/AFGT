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

        List<SelectListItem> listPesquisa = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Artista" },
                new SelectListItem { Value = "2", Text = "Estilo Musical" }
            };

        List<SelectListItem> listOpcao = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Data" },
                new SelectListItem { Value = "2", Text = "Cidade" }
            };

        public ActionResult Index()
        {
            var result = db.Eventos.OrderBy(evento => evento.Data).ToList();

            ViewBag.ListaPesquisa = listPesquisa;
            ViewBag.ListaOpcao = listOpcao;

            return View(result);
        }

        [HttpPost]
        public ActionResult Index(string ConteudoPesquisa, int? GeneroMusicalID, string ListaPesquisa, string Dia, string PointA, string ListaOpcao)
        {
            List<Evento> Evento = new List<Evento>();

            var eventos = db.Eventos.OrderBy(e => e.Data).ToList();

            ViewBag.ListaPesquisa = listPesquisa;
            ViewBag.ListaOpcao = listOpcao;

            switch (ListaPesquisa)
            {
                case "1": //Artista
                    if (ConteudoPesquisa != "")
                    {
                        eventos = eventos.Where(e => e.Artistas.Any(a => a.Nome.ToLower() == ConteudoPesquisa.ToLower())).ToList();
                    }
                    break;

                case "2": //Genero Musical
                    if (GeneroMusicalID != null)
                    {
                        eventos = eventos.Where(e => e.Artistas.Any(a => a.GeneroMusicalID == GeneroMusicalID)).ToList();
                    }
                    break;
            }
            switch (ListaOpcao)
            {
                case "1"://data
                    if (Dia != "")
                    {
                        eventos = eventos.Where(e => e.Data.Value.ToString("yyyy-MM-dd") == Dia).ToList();
                    }
                    break;
                case "2"://cidade
                    if (PointA != "")
                    {
                        eventos = eventos.Where(e => e.Morada.Cidade.ToLower() == PointA.ToLower()).ToList();
                    }
                    break;
            }

            return PartialView("_ResultadosPesquisa", eventos);
        }

        public ActionResult _Local()
        {
            return View();
        }

    }
}