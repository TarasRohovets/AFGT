using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AFGT.Models;

namespace AFGT.Controllers
{
    public class GeneroMusicalsController : Controller
    {
        private afgtEntities db = new afgtEntities();

        // GET: GeneroMusicals
        public ActionResult Index()
        {
            ViewBag.GeneroMusicalID = new SelectList(db.GeneroMusicals.ToList(), "GeneroMusicalID", "NomeEstilo");
            return View();
        }
    }
}
