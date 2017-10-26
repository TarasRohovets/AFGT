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
using Microsoft.AspNet.Identity.Owin;

namespace AFGT.Controllers
{
    public class OrganizadoresController : Controller
    {
        private afgtEntities db = new afgtEntities();

        // GET: Organizadores
        public ActionResult Index()
        {
            return View(db.Organizadores.ToList());
        }

        public ActionResult Menu()
        {
            return View();
        }

        // GET: Organizadores/Details/5
        public ActionResult Details(int? id)    // MENU DO ORGANIZADOR!!!!
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
            Organizadore organizadore = db.Organizadores.Find(id);

            if (organizadore == null)
            {
                return HttpNotFound();
            }
            
            return View(organizadore);
        }
        







        // GET: Organizadores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organizadores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrgID,NomeOrg,Email,Nipc,Password,LinkFotoORG")] Organizadore organizadore)
        {
            if (ModelState.IsValid)
            {
                db.Organizadores.Add(organizadore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(organizadore);
        }

        // GET: Organizadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organizadore organizadore = db.Organizadores.Find(id);
            if (organizadore == null)
            {
                return HttpNotFound();
            }
            return View(organizadore);
        }


        
        
        //[HttpGet]// A metodo para fazer o ulpoad da foto //Tlvz esse aqui vai set o EDIT GET
        //public ActionResult Addphoto()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Edit1(Organizadore imageModel)  //Mudei o nome, nome Edit tem que ser igual para GET e POST
        //{
        //    return View();
        //}

            
        // POST: Organizadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrgID,NomeOrg,Email,Nipc,Password")] Organizadore organizadore, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //
                try
                {
                    if(file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Images/"), _FileName);
                        file.SaveAs(_path);


                        organizadore.LinkFotoORG = "/Images/" + _FileName;
                        db.Entry(organizadore).State = EntityState.Modified;
                        db.SaveChanges();
        
                    }
                    @ViewBag.Message = "Mission Succeded, Congtratulations!";
                    return View(organizadore); //////????? qual return eh aqui?
                }
                catch{
                    @ViewBag.Message = "Abort!Emergency state!File not uploaded!";
                    return View(organizadore);////qual return 
                }
               
              

                return RedirectToAction("Index");
            }
            return View(organizadore);
        }

        
        


        // GET: Organizadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organizadore organizadore = db.Organizadores.Find(id);
            if (organizadore == null)
            {
                return HttpNotFound();
            }
            return View(organizadore);
        }

        // POST: Organizadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organizadore organizadore = db.Organizadores.Find(id);
            db.Organizadores.Remove(organizadore);
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
