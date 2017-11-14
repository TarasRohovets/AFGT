using AFGT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Data.Entity;
using System.Globalization;
using Newtonsoft.Json;

namespace AFGT.Controllers
{
    public class HomeController : Controller
    {
        private afgtEntities db = new afgtEntities();



        //Métodos para Autocomplete da pesquisa (pode ser integrado na pesquisa ou separado). Falta script AJAX na vista e corrigir a chamada da base de dados de Artista para a Lista abaixo


        //[HttpGet]
        //public ActionResult CompletaBusca()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public JsonResult CompletaBusca(string ConteudoPesquisa, string term = "")
        //{
        //    var NomeDeArtista = db.Artistas.Where(c => c.Nome.ToUpper()
        //                    .Contains(term.ToUpper()))
        //                    .Select(c => new { Name = c.Nome, ID = c.ArtistasID })
        //                    .Distinct().ToList();



        //    return Json(NomeDeArtista, JsonRequestBehavior.AllowGet);

        //}


        List<SelectListItem> list = new List<SelectListItem>();
        List<SelectListItem> listPesquisa = new List<SelectListItem>()
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
            ViewBag.ListaPesquisa = list;



            var Artistas = db.Artistas.ToList();
            List<string> Art = new List<string>();
            Artistas.ForEach(a => Art.Add(a.Nome));
            ViewBag.Artistas = JsonConvert.SerializeObject(Art);

            //var Eventos = db.Eventos.ToList();
            //List<string> Eve = new List<string>();
            //Eventos.ForEach(a => Eve.Add(a.NomeEvento));
            //ViewBag.Eventos = JsonConvert.SerializeObject(Eve);


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