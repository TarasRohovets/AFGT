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
            var result = db.Eventos.ToList();

            ViewBag.ListaPesquisa = listPesquisa;
            ViewBag.ListaOpcao = listOpcao;

            switch (ListaPesquisa)
            {
                //result = db.Eventos.ToList();
                case "1":
                    if (ConteudoPesquisa != "")
                    {
                        if (ListaOpcao == "Local")
                        {
                            if (PointA != "")
                            {
                                foreach (Evento item in evento)
                                {
                                    if (item.Morada.Cidade.ToLower() == PointA.ToLower() && item.Artistas.Any(a => a.Nome.ToLower() == ConteudoPesquisa.ToLower()))
                                    {
                                        Evento.Add(item);
                                    }
                                }
                            }
                            else
                            {
                                Evento.Add(item);
                            }

                            result = Evento;
                    }
                    }
                    else
                    {
                        foreach (Evento item in evento)
                        {
                            if (item.Data.Value.ToString("yyyy-MM-dd") == Dia && item.Artistas.Any(a => a.Nome.ToLower() == ConteudoPesquisa.ToLower()))
                            {
                                Evento.Add(item);
                            };
                        }
                        result = Evento;
                    }
                    break;

                case "2":

                    if (GeneroMusicalID == null)
                    {
                        result = evento.ToList();
                    }
                    if ((GeneroMusicalID != null && PointA != "") && ListaOpcao == "Local")
                    {
                        foreach (Evento item in evento)
                        {
                            if (item.Morada.Cidade.ToLower() == PointA.ToLower() && item.Artistas.Any(a => a.GeneroMusicalID == GeneroMusicalID))
                            {
                                Evento.Add(item);
                            };
                        }
                        result = Evento;
                    }
                    else if (!(GeneroMusicalID != null && PointA != "") && ListaOpcao == "Local")
                    {
                        foreach (Evento item in evento)
                        {
                            if (item.Morada.Cidade.ToLower() == PointA.ToLower() || item.Artistas.Any(a => a.GeneroMusicalID == GeneroMusicalID))
                            {
                                Evento.Add(item);
                            };
                        }
                        result = Evento;
                    }
                    else if ((GeneroMusicalID != null && PointA != "") && ListaOpcao == "Data")
                    {
                        foreach (Evento item in evento)
                        {
                            if (item.Data.Value.ToString("yyyy-MM-dd") == Dia && item.Artistas.Any(a => a.GeneroMusicalID == GeneroMusicalID))
                            {
                                Evento.Add(item);
                            };
                        }
                        result = Evento;
                    }
                    else
                    {
                        foreach (Evento item in evento)
                        {
                            if (item.Data.Value.ToString("yyyy-MM-dd") == Dia || item.Artistas.Any(a => a.GeneroMusicalID == GeneroMusicalID))
                            {
                                Evento.Add(item);
                            };
                        }
                        result = Evento;
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