using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AFGT.Models;
using System.IO;

namespace AFGT.Controllers
{
    public class ArtistasController : Controller
    {
        private afgtEntities db = new afgtEntities();

        // GET: Artistas
        public ActionResult Index()
        {
            var artistas = db.Artistas.Include(a => a.GeneroMusical);
            return View(artistas.ToList());
        }

        // GET: Artistas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artista artista = db.Artistas.Find(id);
            if (artista == null)
            {
                return HttpNotFound();
            }
            return View(artista);
        }

        // GET: Artistas/Create
        public ActionResult Create()
        {
            ViewBag.GeneroMusicalID = new SelectList(db.GeneroMusicals, "GeneroMusicalID", "NomeEstilo");
            return View();
        }

        // POST: Artistas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome, LinkFoto, GeneroMusicalID")] Artista artista, HttpPostedFileBase file2)
        {
            if (ModelState.IsValid)
            {
                //criar directorio de imagem de artista
                var path2 = "";
                var _filename2 = "";
                if (file2 != null)
                {
                    if (file2.ContentLength > 0)
                    {
                        //verifica se o ficheiro é imagem
                        if (Path.GetExtension(file2.FileName).ToLower() == ".jpg" ||
                            Path.GetExtension(file2.FileName).ToLower() == ".png" ||
                            Path.GetExtension(file2.FileName).ToLower() == ".jpeg")
                        {
                            _filename2 = Path.GetFileName(file2.FileName);
                            path2 = Path.Combine(Server.MapPath("~/Content/Images/"), _filename2);
                            file2.SaveAs(path2);
                            artista.LinkFoto = "/Content/Images/" + _filename2;
                        }
                    }
                }
                else
                {
                    artista.LinkFoto = "/Content/Images/defaultArt.png";
                }

                //Verificar artista inserido
                var y = db.Artistas.FirstOrDefault(z => z.Nome == artista.Nome);

                //x == null// não existe na base de dados

                if (y != null)
                {
                    artista.ArtistasID = y.ArtistasID;
                    
                }
                else
                {
                    //artista.GeneroMusicalID = genmus.GeneroMusicalID;
                    db.Artistas.Add(artista);
                    db.SaveChanges();

                   

                }

            }

            ViewBag.GeneroMusical = new SelectList(db.GeneroMusicals, "GeneroMusicalID", "NomeEstilo");
            return RedirectToAction("Index");





        }


        // GET: Artistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artista artista = db.Artistas.Find(id);
            if (artista == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneroMusicalID = new SelectList(db.GeneroMusicals, "GeneroMusicalID", "NomeEstilo", artista.GeneroMusicalID);
            return View(artista);
        }

        // POST: Artistas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtistasID,Nome,LinkFoto,GeneroMusicalID")] Artista artista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GeneroMusicalID = new SelectList(db.GeneroMusicals, "GeneroMusicalID", "NomeEstilo", artista.GeneroMusicalID);
            return View(artista);
        }

        // GET: Artistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artista artista = db.Artistas.Find(id);
            if (artista == null)
            {
                return HttpNotFound();
            }
            return View(artista);
        }

        // POST: Artistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artista artista = db.Artistas.Find(id);
            db.Artistas.Remove(artista);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
