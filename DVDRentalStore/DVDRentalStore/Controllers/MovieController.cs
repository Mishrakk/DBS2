using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DVDRentalStore.Models;
using DVDRentalStore.DAL;
using System.Data.Entity;

namespace DVDRentalStore.Controllers
{
    public class MovieController : Controller
    {
        private readonly StoreContext db = new StoreContext();
        // GET: Movie
        public ActionResult Index(string sortOrder)
        {
            IEnumerable<Movie> movs;
            ViewBag.NextSortOrder = sortOrder == null || sortOrder == "descending" ? "ascending" : "descending";
            switch (sortOrder)
            {
                case "ascending":
                    movs = db.Movies.Include(movie => movie.Copies).OrderBy(movie => movie.Title);
                    break;
                case "descending":
                    movs = db.Movies.Include(movie => movie.Copies).OrderByDescending(movie => movie.Title);
                    break;
                default:
                    movs = db.Movies.Include(movie => movie.Copies);
                    break;
            }
            return View(movs);
        }

        // POST: Movie
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string searchString = collection["searchString"];
            ViewBag.currentSearchString = searchString;
            var movs = db.Movies.Include(movie => movie.Copies).Where(movie => movie.Title.Contains(searchString));
            return View(movs);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Movie newMovie = new Movie { Title = collection["Title"], Year = int.Parse(collection["Year"]) };
                db.Movies.Add(newMovie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var movie = db.Movies.Single(obj => obj.MovieId == id);
            return View(movie);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var movie = db.Movies.Single(obj => obj.MovieId == id);
                movie.Title = collection["Title"];
                movie.Year = int.Parse(collection["Year"]);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
