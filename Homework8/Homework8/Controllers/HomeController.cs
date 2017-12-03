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
            
            //viewbags so that we can populate the fields in our edit field so that data that wont be changed
            // doesnt have to be retyped
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
        ///Since we get an integer relating to the ID of a particular genre we can use that to search 
        ///for the various pieces of artwork that correspond to this key. 
        ///Thus using a little bit of lambda we can cook a good stew.
        /// </summary>

        // POST: Home/Genre
        [HttpGet]
        public JsonResult GenreResult(int? id)
        {
            //data checking for sanity
            if (id == null)
            {
                return null;
            }

            //our JSON object that will be returned
            var artCollection = db.Genres.Where(g => g.GenreID == id) //getting the Genre from the ID
                                .Select(x => x.Classifications) //Getting the classifications that have that genre
                                .FirstOrDefault()//making sure we can find the head of the list
                                .Select(x => new { x.ArtWork.Title, x.ArtWork.Artist.ArtistName }) //Get the title of the artwork and the artist name
                                .OrderBy(x => x.Title) //abc ordering
                                .ToList(); //return it as a list type instead of an enumerable.
            return Json(artCollection, JsonRequestBehavior.AllowGet); //return the object to the CustomJS.js JavaAJAX_Call function.
        }
    
}
}
    
