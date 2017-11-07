using AFGT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;

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

            //string ConteudoPesquisa = Request.Form["ConteudoPesquisa"].ToString();
            //string TipoPesquisa = Request.Form["ListaPesquisa"].ToString();


            
            //GetAuthURI();
            return View();
        }

        [HttpPost, ActionName("Index")]
        public ActionResult Data(DateTime Dia, string TipoPesquisa, string ConteudoPesquisa)
        {
            List<Models.Evento> Evento = new List<Models.Evento>();

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

        [HttpPost, ActionName("Index")]
        public ActionResult Local(string PointA, string TipoPesquisa, string ConteudoPesquisa)
        {
            List<Evento> Evento = new List<Evento>();

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











        //TENTIVA DE METODOS PARA POR OS PEDIDOS AO API A FUNCIONAR


        //public ActionResult Busca(string filtro, string inputDePesquisa)
        //{
        //    switch (filtro)
        //    {
        //        case "Artist":
        //            { 
        //     var inp = SpotifyWebAPI.Artist.Search(inputDePesquisa).Result;
        //                return ViewBag.Message = inp.ToString();
        //     //var temp = SpotifyWebAPI.Artist.GetArtist(inputDePesquisa);
        //            }
        //        case "Event":
        //            {
        //                return ViewBag.Message ="TestandoaBuscadeEventos";
        //            }


        //    }


        //        return View(); //How many results are there in total? NOTE: item.Tracks = item.Artists = null
        //    }

        //public async Task<ActionResult> Busca()
        //{

        //    var artSearch = await SpotifyWebAPI.Artist.Search();
        //    var temp;
        //    var output = await SpotifyWebAPI.Artist.GetArtist();
            
            

        //    return View(output);

        //}

        //
        //public ActionResult getArtist()
        //{
        //    string token = SpotifyWebAPI.Authentication.ClientId{ };
        //    return string bla;
        //}



        ////isto obtem a autentificacao do URI do spotify

        //private string GetAuthURI()
        //{
        //    string clientID ="382266817b35478eba309a571cb4c22b";
        //    //string redirectURI = "http://localhost:50210/Home/AuthResponse";
        //    //Scope scope = Scope.PLAYLIST_MODIFY_PRIVATE;

        //    return "https://accounts.spotify.com/en/authorize?client_id=" + clientID "&response_type=token&redirect_uri=" + redirectUrl + "&state=&scope=" + scope.GetStringAttribute(" ") + "&show_dialog=true";
        //}

    }
}