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
        private static List<Movie> movies = new List<Movie>
        {
            new Movie(1, "Example title 1", 2000),
            new Movie(2, "Example title 2", 2001)
        };
        // GET: Movie
        public ActionResult Index()
        {
            return View(movies);
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
