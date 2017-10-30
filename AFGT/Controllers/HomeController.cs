using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyWebAPI.SpotifyModel;
using System.Threading.Tasks;

namespace AFGT.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            
            //GetAuthURI();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
        //[HttpGet]
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
            

        //    var output = await SpotifyWebAPI.Artist.GetArtist("Beatles");
            
            

        //    return View(output);

        //}

        //[HttpGet]
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