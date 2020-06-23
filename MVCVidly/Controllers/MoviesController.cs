using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVidly.Models;
using System.Data.Entity;
using MVCVidly.ViewModel;

namespace MVCVidly.Controllers
{
    public class MoviesController : Controller
    {
        public ApplicationDbContext _context;

        //create constructor for to allocate instance
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //create Dispose for dispose the instance
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies
        public ActionResult Index()
        {
            var MovieList = _context.Movies.Include(m=>m.Genre).ToList();
            return View(MovieList);
        }

        public ActionResult CreateMovie()
        {
            var genreList = _context.Genres.ToList();
            NewMovieViewModel newMovieViewModel = new NewMovieViewModel()
            {
                Genres = genreList,
                Movie = new Movie()


            };
            return View(newMovieViewModel);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}