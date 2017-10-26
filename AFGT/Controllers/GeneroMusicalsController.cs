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

        // GET: GeneroMusicals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneroMusical generoMusical = db.GeneroMusicals.Find(id);
            if (generoMusical == null)
            {
                return HttpNotFound();
            }
            return View(generoMusical);
        }

        // GET: GeneroMusicals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GeneroMusicals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GeneroMusicalID,NomeEstilo")] GeneroMusical generoMusical)
        {
            if (ModelState.IsValid)
            {
                db.GeneroMusicals.Add(generoMusical);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(generoMusical);
        }

        // GET: GeneroMusicals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneroMusical generoMusical = db.GeneroMusicals.Find(id);
            if (generoMusical == null)
            {
                return HttpNotFound();
            }
            return View(generoMusical);
        }

        // POST: GeneroMusicals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GeneroMusicalID,NomeEstilo")] GeneroMusical generoMusical)
        {
            if (ModelState.IsValid)
            {
                db.Entry(generoMusical).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(generoMusical);
        }

        // GET: GeneroMusicals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneroMusical generoMusical = db.GeneroMusicals.Find(id);
            if (generoMusical == null)
            {
                return HttpNotFound();
            }
            return View(generoMusical);
        }

        // POST: GeneroMusicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GeneroMusical generoMusical = db.GeneroMusicals.Find(id);
            db.GeneroMusicals.Remove(generoMusical);
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
