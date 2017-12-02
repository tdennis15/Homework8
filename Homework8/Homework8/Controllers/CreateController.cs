using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework8.Models;

namespace Homework8.Controllers
{
    public class CreateController : Controller
    {

        private ArtContext db = new ArtContext();

        // GET: Create
        public ActionResult Index()
        {
            return View();
        }

        // GET: Create an artist
        public ActionResult ArtistCreate()
        {
            return View();
        }

        // POST: creation of artists
        [HttpPost]
        public ActionResult ArtistCreate(FormCollection collection)
        {
            try
            {
                Artist artist = db.Artists.Create();

                artist.ArtistName = collection["artistName"];
                artist.BirthCity = collection["birthCity"];
                artist.BirthCountry = collection["birthCountry"];
                artist.DOB = collection["birthDate"];

                db.Artists.Add(artist);
                db.SaveChanges();

                return RedirectToAction("Artists", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}