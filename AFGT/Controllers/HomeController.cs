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

            var evento = db.Eventos.OrderBy(e => e.Data).ToList();

            ViewBag.ListaPesquisa = listPesquisa;
            ViewBag.ListaOpcao = listOpcao;

            switch (ListaPesquisa)
            {
                case "1":
                    if (ConteudoPesquisa != "")
                    {
                        evento = evento.Where(e => e.Artistas.Any(a => a.Nome.ToLower() == ConteudoPesquisa.ToLower())).ToList();

                        if (ListaOpcao == "2")
                        {
                            if (PointA != "")
                            {
                                evento = evento.Where(e => e.Morada.Cidade.ToLower() == PointA.ToLower()).ToList();
                            }
                        }

                        if (ListaOpcao == "1")
                        {
                            if (Dia != "")
                            {
                                evento = evento.Where(e => e.Data.Value.ToString("yyyy-MM-dd") == Dia).ToList();
                            }
                        }
                    }

                    if (ConteudoPesquisa == "")
                    {
                        if (ListaOpcao == "2")
                        {
                            if (PointA != "")
                            {
                                evento = evento.Where(e => e.Morada.Cidade.ToLower() == PointA.ToLower()).ToList();
                            }
                        }

                        if (ListaOpcao == "1")
                        {
                            if (Dia != "")
                            {
                                evento = evento.Where(e => e.Data.Value.ToString("yyyy-MM-dd") == Dia).ToList();
                            }
                        }
                    }

                    break;

                case "2":
                    if (GeneroMusicalID != null)
                    {
                        evento = evento.Where(e => e.Artistas.Any(a => a.GeneroMusicalID == GeneroMusicalID)).ToList();

                        if (ListaOpcao == "2")
                        {
                            if (PointA != "")
                            {
                                evento = evento.Where(e => e.Morada.Cidade.ToLower() == PointA.ToLower()).ToList();
                            }
                        }

                        if (ListaOpcao == "1")
                        {
                            if (Dia != "")
                            {
                                evento = evento.Where(e => e.Data.Value.ToString("yyyy-MM-dd") == Dia).ToList();
                            }
                        }
                    }

                    if (GeneroMusicalID != null)
                    {
                        if (ListaOpcao == "2")
                        {
                            if (PointA != "")
                            {
                                evento = evento.Where(e => e.Morada.Cidade.ToLower() == PointA.ToLower()).ToList();
                            }
                        }

                        if (ListaOpcao == "1")
                        {
                            if (Dia != "")
                            {
                                evento = evento.Where(e => e.Data.Value.ToString("yyyy-MM-dd") == Dia).ToList();
                            }
                        }
                    }

                    break;
            }

            return PartialView("_ResultadosPesquisa", evento);
        }

        public ActionResult _Local()
        {
            return View();
        }

    }
}