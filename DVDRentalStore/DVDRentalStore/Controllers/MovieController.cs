using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DVDRentalStore.Models;

namespace DVDRentalStore.Controllers
{
    public class MovieController : Controller
    {
        private static IEnumerable<Movie> movies = new List<Movie> {
            new Movie(1, "Anchorman", 2000, new List<Copy>{ new Copy(1, true, 1), new Copy(2, true, 1), new Copy(3, false, 1)}),
            new Movie(2, "Anchorman 2", 2001),
            new Movie(3, "Terminator", 1993),
            new Movie(4, "Jurrasic Park", 1999, new List<Copy>{ new Copy(1, true, 1), new Copy(2, false, 1), new Copy(3, true, 1)}),
            new Movie(5, "The Lord of the Rings", 1997),
        };
        // GET: Movie
        public ActionResult Index(string sortOrder)
        {
            IEnumerable<Movie> movs;
            ViewBag.NextSortOrder = sortOrder == null || sortOrder == "descending" ? "ascending" : "descending";
            switch (sortOrder)
            {
                case "ascending":
                    movs = movies.OrderBy(movie => movie.Title);
                    break;
                case "descending":
                    movs = movies.OrderByDescending(movie => movie.Title);
                    break;
                default:
                    movs = movies;
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
            var movs = movies.Where(movie => movie.Title.Contains(searchString));
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
                // TODO: Add insert logic here

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
            var movie = movies.Single(obj => obj.MovieId == id);
            return View(movie);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                movies.Single(obj => obj.MovieId == id).Title = collection["Title"];

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
