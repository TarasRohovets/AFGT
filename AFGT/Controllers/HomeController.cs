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

        [HttpPost]
        public  ActionResult Index(string ConteudoPesquisa, string GeneroMusicalID, string ListaPesquisa, DateTime Dia, string PointA, string TipoOpcao)
        {
            List<Evento> Evento = new List<Evento>();

            switch (TipoOpcao) {
                case "Data":
                    while (Dia==null)
                    {
                        return View(db.Eventos.ToList());
                    }
                    if (ConteudoPesquisa == null)
                    {
                        return View(db.Eventos.Where(model => model.Data == Dia || Dia == null).ToList());
                    }
                    else if (ListaPesquisa == "Estilo Musical")
                    {
                        return View(db.Eventos.Where(model => model.Data == Dia || Dia == null).ToList()); //.Where(model => model.Artistas1.GeneroMusical.NomeEstilo.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
                    }
                        else
                        {
                           return View(db.Eventos.Where(model => model.Data == Dia || Dia == null).ToList().Where(model => model.Artistas.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
                        }          

                case "Local":
                    if (GeneroMusicalID == null)
                    {
                        return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList());
                    }
                    else if (ListaPesquisa == "Estilo Musical")
                    {
                        return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList());//.Where(model => model.Artistas.GeneroMusical.NomeEstilo.ToLower() == GeneroMusicalID.ToLower() || GeneroMusicalID == null));
                    }
                        else
                        {
                          return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList().Where(model => model.Artistas.ToLower() == GeneroMusicalID.ToLower() || GeneroMusicalID == null));
                        }
            }
            return View();
        }
    }
        //[HttpPost, ActionName("Index")]
        //public ActionResult Data(DateTime Dia,string ConteudoPesquisa, string ListaPesquisa)
        //{
        //    string TipoPesquisa = Request["ListaPesquisa"].ToString();
        //    string ConteudoPesquisa = Request["ConteudoPesquisa"].ToString();

        //    List<Evento> Evento = new List<Evento>();

        //    if (ConteudoPesquisa == null)
        //    {
        //        return View(db.Eventos.Where(model => model.Data == Dia || Dia == null).ToList());
        //    }
        //    else
        //    {
        //        if (TipoPesquisa == "Estilo Musical")
        //        {
        //            return View(db.Eventos.Where(model => model.Data == Dia || Dia == null).ToList()); //.Where(model => model.Artistas1.GeneroMusical.NomeEstilo.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
        //        }
        //        else
        //        {
        //            return View(db.Eventos.Where(model => model.Data == Dia || Dia == null).ToList().Where(model => model.Artistas.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
        //        }           
        //    }
        //}

        //[HttpPost, ActionName("Index")]
        //public ActionResult Local(string PointA, string ConteudoPesquisa, string ListaPesquisa,)
        //{
        //    string TipoPesquisa = Request["TipoPesquisa"].ToString();
        //    string ConteudoPesquisa = Request["ConteudoPesquisa"].ToString();

        //    List<Evento> Evento = new List<Evento>();

        //    if (ConteudoPesquisa == null)
        //    {
        //        return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList());
        //    }
        //    else
        //    {

        //        if (TipoPesquisa == "Estilo Musical")
        //        {
        //            return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList());//.Where(model => model.Artistas.GeneroMusical.NomeEstilo.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
        //        }
        //        else
        //        {
        //            return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList().Where(model => model.Artistas.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
        //        }
        //    }
        //}
   
}