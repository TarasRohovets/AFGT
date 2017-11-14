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
using Newtonsoft.Json;

namespace AFGT.Controllers
{
    public class EventoesController : Controller
    {
        private afgtEntities db = new afgtEntities();
        

        // GET: Eventoes
        public ActionResult Index()
        {
            var eventos = db.Eventos.Include(e => e.Organizadore);
            
            var usid = Convert.ToInt32(User.Identity.GetUserId());

            

            return View(eventos.Where(ev => ev.OrgID == usid).ToList());
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

            //nao possibilita a alteracao de artistas
            var ArtistasDoEv = evento.Artistas.ToList();
            List<string> Art = new List<string>();
            ArtistasDoEv.ForEach(a => Art.Add(a.Nome));
            ViewBag.Artistas = JsonConvert.SerializeObject(Art);
           

        
            ViewBag.users = db.AspNetUsers.Where(e => e.Likes.Any(l => l.EventosID == id));   //

            // ViewBag.users = db.AspNetUsers.Where(l => l.Likes.Any(l => l.UserID == userId));

            return View(evento);
        }


        [HttpPost]                       //SHIIT LOOOOOOOOOOOOLmLIKESSSS       
        public ActionResult Like(int id, string Opiniao)    // Preencho isto ou fica vasio???!!!!!!
        {
            if (ModelState.IsValid )
            {
                // var userID = Convert.ToInt32(User.Identity.GetUserId());
                // ViewBag.QualquerCoisa = userID;
                var userID = Convert.ToInt32(User.Identity.GetUserId());
                var x = db.Likes.FirstOrDefault(m => m.UserID == userID && m.EventosID == id);
                                                                                          // opiniao como       ???????????????????????????
                if(x == null)
                {
                    Like like = new Like(); //vai dentro do if
                   
                    like.UserID = userID; //  id do user
                    like.EventosID = id;   //        id do evento 
                    if (Opiniao == "Like")    //  se opiniao yes 
                    {
                        like.Opiniao = true;
                    }
                    if (Opiniao == "DisLike")    //  se opiniao yes 
                    {
                        like.Opiniao = false;
                    }
                    db.Likes.Add(like);
                    db.SaveChanges();

                    return PartialView("UsersList", db.Likes.Include("AspNetUser").Where(l => l.EventosID == id));
                }

            };

            return HttpNotFound();
        }
        









        // GET: Eventoes/Create
        public ActionResult Create()
        {

            var Artistas = db.Artistas.ToList();
            List<string> Art = new List<string>();
            Artistas.ForEach(ar => Art.Add(ar.Nome));
            ViewBag.Artistas = JsonConvert.SerializeObject(Art);
            ViewBag.OrgID = new SelectList(db.Organizadores, "OrgID", "NomeOrg");
            return View();
        }








        // POST: Eventoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NomeEvento,Descricao,Data,Link")] Evento evento, HttpPostedFileBase file, [Bind(Include = "Endereco,Cidade,CodPostal")] Morada morada, string entradadetags)
        {


=======
        public ActionResult Create([Bind(Include = "NomeEvento,Descricao,Data,Link")] Evento evento, HttpPostedFileBase file, [Bind(Include = "Endereco,Cidade,CodPostal")] Morada morada, HttpPostedFileBase[] files, [Bind(Include = "EventoID,FotoURL")] FotoGallery fotoGallery)
        {
            /*imagem principal*/
>>>>>>> master
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
                }
            } else
            {
                evento.Link = "/Content/Images/default.jpg";
            }




            /*fim imagem principal*/
            
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

            
        
            //recebe artistas da vista e cria listagem dos tags
            //List<string> listagem = new List<string>();
            //listagem = artistas;



           


                evento.OrgID = Convert.ToInt32(User.Identity.GetUserId());
                //evento.OrgID = 1;
                db.Eventos.Add(evento);
                db.SaveChanges();

            //separacao do string de entrada em varios strings
            
            string[] nova =  entradadetags.ToString().Split(',');

            //for (int y = 0; y < values.Length; y++) {
            //    string[] nova = nova.Add(values);
            //}
            


            foreach (var a in nova)
            {

                var y = db.Artistas.FirstOrDefault(at => at.Nome == a);
                if (y != null)
                {

                    evento.Artistas.Add(y);
                }
                else
                {
                    Artista naocriado = new Artista();
                    naocriado.Nome = a;
                    naocriado.GeneroMusicalID = 38;
                    naocriado.LinkFoto = "/Content/Images/defaultArt.png";

                    db.Artistas.Add(naocriado);
                    db.SaveChanges();
                    evento.Artistas.Add(naocriado);
                    db.SaveChanges();

                    //TempData["msg"] = "<script>alert('Hi this is a message');</script>";


                }

            if (files != null)
            {
                foreach (var foto in files)
                {
                    var _fotopath = "";
                    var _fotoFileName = "";
                    if (foto != null)
                    {
                        if (foto.ContentLength > 0)
                        {
                            //verifica se o ficheiro é imagem
                            if (Path.GetExtension(foto.FileName).ToLower() == ".jpg" ||
                                Path.GetExtension(foto.FileName).ToLower() == ".png" ||
                                Path.GetExtension(foto.FileName).ToLower() == ".jpeg")
                            {

                                _fotoFileName = Path.GetFileName(foto.FileName);
                                _fotopath = Path.Combine(Server.MapPath("~/Content/Images/"), _fotoFileName);
                                foto.SaveAs(_fotopath);
                                fotoGallery.FotoURL = "/Content/Images/" + _fotoFileName;
                                fotoGallery.EventoID = evento.EventosID;
                                db.FotoGalleries.Add(fotoGallery);
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }




            return RedirectToAction("Index");
          
           
          
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

          
            //mostra artistas em tags
            var ArtistasDoEv = evento.Artistas.ToList();
            List<string> Art = new List<string>();
            ArtistasDoEv.ForEach(a => Art.Add(a.Nome));
            ViewBag.Artistas = Art;


            ViewBag.OrgID = new SelectList(db.Organizadores, "OrgID", "NomeOrg", evento.OrgID);
            return View("Index");
        }

        

        // POST: Eventoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrgID,EventosID,NomeEvento,Descricao,Data,Link")] Evento evento, HttpPostedFileBase file, [Bind(Include = "Endereco,Cidade,CodPostal")] Morada morada, List<string> artistas)
        public ActionResult Edit([Bind(Include = "OrgID,EventosID,NomeEvento,Descricao,Data,Link")] Evento evento, HttpPostedFileBase file, [Bind(Include = "Endereco,Cidade,CodPostal")] Morada morada, HttpPostedFileBase[] files, [Bind(Include = "EventoID,FotoURL")] FotoGallery fotoGallery)
        {
            if (ModelState.IsValid)
            {
                /*verificaçao morada inserida*/
                var _path = "";
                var _FileName = "";
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        //verifica se o ficheiro é imagem
                        if (Path.GetExtension(file.FileName).ToLower() == ".jpg" ||
                            Path.GetExtension(file.FileName).ToLower() == ".png" ||
                            Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                        {

                            _FileName = Path.GetFileName(file.FileName);
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
                /*Fim de verificaçao morada inserida*/
                if (files != null)
                {
                    foreach (var foto in files)
                    {
                        var _fotopath = "";
                        var _fotoFileName = "";
                        if (foto != null)
                        {
                            if (foto.ContentLength > 0)
                            {
                                //verifica se o ficheiro é imagem
                                if (Path.GetExtension(foto.FileName).ToLower() == ".jpg" ||
                                    Path.GetExtension(foto.FileName).ToLower() == ".png" ||
                                    Path.GetExtension(foto.FileName).ToLower() == ".jpeg")
                                {

                                    _fotoFileName = Path.GetFileName(foto.FileName);
                                    _fotopath = Path.Combine(Server.MapPath("~/Content/Images/"), _fotoFileName);
                                    foto.SaveAs(_fotopath);
                                    fotoGallery.FotoURL = "/Content/Images/" + _fotoFileName;
                                    fotoGallery.EventoID = evento.EventosID;
                                    db.FotoGalleries.Add(fotoGallery);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
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
                //evento.OrgID = 1;
                /*oraganizadores*/


                // mostra artistas

                var ArtistasDoEv = evento.Artistas.ToList();
                List<string> Art = new List<string>();
                ArtistasDoEv.ForEach(a => Art.Add(a.Nome));
                ViewBag.Artistas = Art;

                //recebe artistas da vista e cria listagem dos tags
                List<string> listagem = new List<string>();
                listagem = artistas;


                
                foreach (var a in listagem) {
                   
                       var y = db.Artistas.FirstOrDefault(at => at.Nome == a);
                    if (y != null)
                    {
                        evento.Artistas.Add(y);
                    }
                    else
                    {
                        Artista naocriado = new Artista();
                        naocriado.Nome = a;
                        naocriado.GeneroMusicalID = 38;
                        naocriado.LinkFoto = "/Content/Images/defaultArt.png"; ;

                        db.Artistas.Add(naocriado);
                        db.SaveChanges();
                        evento.Artistas.Add(naocriado);
                        db.SaveChanges();


                    }
                    
                }

                //db.Entry(listagem).State = EntityState.Modified; ////altera 
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.OrgID = new SelectList(db.Organizadores, "OrgID", "NomeOrg", evento.OrgID);
            return View("Index");
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
