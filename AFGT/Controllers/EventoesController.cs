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
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace AFGT.Controllers
{
    public class EventoesController : Controller
    {
        private afgtEntities db = new afgtEntities();
    

        // GET: Eventoes
        public ActionResult Index()
        {
            var eventos = db.Eventos.Include(e => e.Organizadore);
            return View(eventos.ToList());
        }

       

        // GET: Eventoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Eventos.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // GET: Eventoes/Create
        public ActionResult Create()
        {
            ViewBag.OrgID = new SelectList(db.Organizadores, "OrgID", "NomeOrg");
            return View();
        }

        // POST: Eventoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NomeEvento,Descricao,Data,Artistas,Link")] Evento evento, HttpPostedFileBase file, [Bind(Include = "Endereco,Cidade,CodPostal")] Morada morada)
        {
<<<<<<< HEAD

            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Images/"), _FileName);
                    file.SaveAs(_path);
                    

                    evento.Link = "/Images/" + _FileName;      //////    Adiciono o link a tabela AspNetUsers
                    
                    db.Entry(evento).State = EntityState.Modified;      /////     Faz Alteracoes na Base de Dados 
                    db.SaveChanges();                                      /////     Grava as altereacoes 

=======
            var _path = "";
            var _FileName = "";
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    //verifica se o ficheiro é imagem
                    if(Path.GetExtension(file.FileName).ToLower()==".jpg" ||
                        Path.GetExtension(file.FileName).ToLower() == ".png" ||
                        Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                    {

                        _FileName = Path.GetFileName(file.FileName);
                        _path = Path.Combine(Server.MapPath("~/Content/Images/"), _FileName);
                        file.SaveAs(_path);
                        evento.Link = "/Content/Images/" + _FileName;
                    }
>>>>>>> BranchF
                }
                @ViewBag.Message = "Mission Succeded, Congtratulations!";
                return View(evento); //////????? qual return eh aqui?
            }
            catch
            {
<<<<<<< HEAD
                @ViewBag.Message = "Abort!Emergency state!File not uploaded!";
                return View(evento);////qual return 
=======
                evento.Link = "/Content/Images/default.jpg";
>>>>>>> BranchF
            }

            /*Verificar morada inserida*/
            var x = db.Moradas.FirstOrDefault(m => m.Endereco == morada.Endereco &&  m.CodPostal == morada.CodPostal && m.Cidade == morada.Cidade);

            //x == null// não existe na base de dados

            if ( x != null)
            {
                evento.MoradaID = x.MoradaID;
               

            }
            else
            {                
                db.Moradas.Add(morada);
                db.SaveChanges();
                evento.MoradaID = morada.MoradaID;
            }
            /*Fim de verificaçao morada inserida*/

<<<<<<< HEAD
            evento.OrgID = Convert.ToInt32(User.Identity.GetUserId());
            //var userId = User.Identity.GetUserId<int>();
            //evento.OrgID = 1;
=======

           



            //evento.OrgID = Convert.ToInt32(User.Identity.GetUserId());
            evento.OrgID = 1;
>>>>>>> BranchF
                db.Eventos.Add(evento);
                db.SaveChanges();
                return RedirectToAction("Index");
          
           
            //ViewBag.OrgID = new SelectList(db.Organizadores, "OrgID", "NomeOrg", evento.OrgID);
            //return View(evento);
        }
        
        // GET: Eventoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Eventos.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }

           


            ViewBag.OrgID = new SelectList(db.Organizadores, "OrgID", "NomeOrg", evento.OrgID);
            return View(evento);
        }

        // POST: Eventoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventosID,OrgID,NomeEvento,Morada,Descricao,Data,Artistas,Link")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                            _path = Path.Combine(Server.MapPath("~/Content/Images/"), _FileName);
                            file.SaveAs(_path);
                            evento.Link = "/Content/Images/" + _FileName;
                        }
                    }
                }
                else
                {
                    evento.Link = "/Content/Images/default.jpg";
                }

                /*Verificar morada inserida*/
                var x = db.Moradas.FirstOrDefault(m => m.Endereco == morada.Endereco && m.CodPostal == morada.CodPostal && m.Cidade == morada.Cidade);

                //x == null// não existe na base de dados

                if (x != null)
                {
                    evento.MoradaID = x.MoradaID;


                }
                else
                {

                    db.Moradas.Add(morada);
                    db.SaveChanges();
                    evento.MoradaID = morada.MoradaID;
                }
                /*Fim de verificaçao morada inserida*/

                /*organizadores*/
                //evento.OrgID = Convert.ToInt32(User.Identity.GetUserId());
                evento.OrgID = 1;
                /*oraganizadores*/

                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.OrgID = new SelectList(db.Organizadores, "OrgID", "NomeOrg", evento.OrgID);
            return View(evento);
        }

        // GET: Eventoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Eventos.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: Eventoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evento evento = db.Eventos.Find(id);
            db.Eventos.Remove(evento);
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
