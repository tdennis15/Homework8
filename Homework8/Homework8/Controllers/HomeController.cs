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

        public ActionResult Index()
        {

            var genres = db.Genres;
            return View(genres);
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


        /// <summary>
        /// Using the following link to understand what the controller and ajax and jquery were needing to talk helped a lot
        ///     https://stackoverflow.com/questions/25304610/how-to-get-a-list-from-mvc-controller-to-view-using-jquery-ajax
        /// Since we need to pass a list to the home page instead of a single value means we need to use arrays and loops
        /// to iterate over the values and convert to a means that the system can handle. 
        /// This is after the CustomJS.js file has asked for a return. 
        /// </summary>

        [HttpPost]
        public JsonResult Genre(string genre)
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