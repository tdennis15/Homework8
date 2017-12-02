using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework8.Models;

namespace Homework8.Controllers
{
    public class HomeController : Controller
    {
        private ArtContext db = new ArtContext();

        //GET: /Index
        //Return Genres
        public ActionResult Index()
        {

            var genres = db.Genres;
            return View(genres);

        }

        //GET: /Artists
        //Return Artists
        public ActionResult Artists()
        {
            var artists = db.Artists;
            return View(artists);
        }

        // GET: /Artworks
        public ActionResult Artworks()
        {
            var artworks = db.ArtWorks;
            return View(artworks);
        }

        // GET: /Classifications
        public ActionResult Classifications()
        {
            var classifications = db.Classifications;
            return View(classifications);
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

                return RedirectToAction("Artists");
            }
            catch
            {
                return View("Artists");
            }
        }


        // GET: Details about an artist 
        public ActionResult Details(int id)
        {
            var artist = db.Artists.Where(a => a.ID == id).FirstOrDefault();
            return View(artist);
        }


        // GET: Edit Artist Details
        public ActionResult Edit(int id)
        {
            ViewBag.aName = db.Artists.Where(a => a.ID == id).FirstOrDefault().ArtistName;
            ViewBag.aCity = db.Artists.Where(a => a.ID == id).FirstOrDefault().BirthCity;
            ViewBag.aCountry = db.Artists.Where(a => a.ID == id).FirstOrDefault().BirthCountry;
            ViewBag.aDOB = db.Artists.Where(a => a.ID == id).FirstOrDefault().DOB;
            return View();
        }

        // POST: Update the database with the posted details
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var artistToUpdate = db.Artists.Find(id);

                artistToUpdate.ArtistName = collection["artistName"];
                artistToUpdate.BirthCity = collection["birthCity"];
                artistToUpdate.BirthCountry = collection["BirthCountry"];
                artistToUpdate.DOB = collection["birthDate"];

                db.SaveChanges();

                return RedirectToAction("Details/" + id);
            }
            catch
            {
                return View();
            }
        }

        // GET: Form to delete an artist
        public ActionResult Delete(int id)
        {
            var artist = db.Artists.Where(a => a.ID == id).FirstOrDefault();

            ViewBag.aName = db.Artists.Where(a => a.ID == id).FirstOrDefault().ArtistName;
            ViewBag.aCity = db.Artists.Where(a => a.ID == id).FirstOrDefault().BirthCity;
            ViewBag.aCountry = db.Artists.Where(a => a.ID == id).FirstOrDefault().BirthCountry;
            ViewBag.aDOB = db.Artists.Where(a => a.ID == id).FirstOrDefault().DOB;

            return View(artist);
        }

        // POST: Deleting the artist from DB
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var artist = db.Artists.Find(id);

                db.Artists.Remove(artist);
                db.SaveChanges();

                return RedirectToAction("Artists");
            }
            catch
            {
                return View();
            }
        }



        /// <summary>
        /// Using the following link to understand what the controller and ajax and jquery were needing to talk helped a lot
        ///     https://stackoverflow.com/questions/25304610/how-to-get-a-list-from-mvc-controller-to-view-using-jquery-ajax
        /// Since we need to pass a list to the home page instead of a single value means we need to use arrays and loops
        /// to iterate over the values and convert to a means that the system can handle. 
        /// This is after the CustomJS.js file has asked for a return. 
        /// </summary>

        [HttpPost]
        public JsonResult GenreResult(string genre)
        {
            var artwork = db.Genres.Find(genre).Classifications.ToList().OrderBy(t => t.ArtWork.Title).Select(a => new { aw = a.AWID, awa = a.ArtWork.ArtistID }).ToList();
            string[] artworkCreator = new string[artwork.Count()];
            for (int i = 0; i < artworkCreator.Length; ++i)
            {
                artworkCreator[i] = $"<ul>{db.ArtWorks.Find(artwork[i].aw).Title} by {db.Artists.Find(artwork[i].awa).ArtistName}</ul>";
            }
            var data = new
            {
                arr = artworkCreator
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}