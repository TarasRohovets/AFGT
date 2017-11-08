using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AFGT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
            while (ConteudoPesquisa != null)
            { 
                if (TipoPesquisa == "Estilo Musical")
                {
                    return View(db.Eventos.Where(model => model.Data == Dia || Dia == null).ToList());//.Where(model => model.Artistas.GeneroMusical.NomeEstilo.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
                }
                else
                {
                    return View(db.Eventos.Where(model => model.Data == Dia || Dia == null).ToList());//.Where(model => model.Artistas.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
                }
            }

            return View(db.Eventos.Where(model => model.Data == Dia || Dia == null).ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
            while (ConteudoPesquisa != null)
            {
                if (TipoPesquisa == "Estilo Musical")
                {
                    return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList());//.Where(model => model.Artistas.GeneroMusical.NomeEstilo.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
                }
                else
                {
                    return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList());//.Where(model => model.Artistas.ToLower() == ConteudoPesquisa.ToLower() || ConteudoPesquisa == null));
                }
            }

            return View(db.Eventos.Where(model => model.Morada.Cidade.ToLower() == PointA.ToLower() || PointA == null).ToList());
        }
    }
}