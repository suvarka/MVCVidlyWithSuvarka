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
            //var MovieList = _context.Movies.Include(m=>m.Genre).ToList();
            //return View(MovieList);

            if (User.IsInRole(RoleName.CanManageMovies))
                return View("MoviesList");
            return View("ReadOnlyMovies");

        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult CreateMovie()
        {
            var genreList = _context.Genres.ToList();
            MovieFormViewModel newMovieViewModel = new MovieFormViewModel()
            {
                Genres = genreList,
                Movie = new Movie()


            };
            return View("MovieForm", newMovieViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var genreList = _context.Genres.ToList();
                MovieFormViewModel newMovieViewModel = new MovieFormViewModel()
                {
                    Genres = genreList,
                    Movie = new Movie()


                };
                return View("MovieForm", newMovieViewModel);

            }

            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieDetails = _context.Movies.Where(m => m.Id == movie.Id).FirstOrDefault();
                movieDetails.Name = movie.Name;
                movieDetails.GenreId = movie.GenreId;
                movieDetails.DateAdded = movie.DateAdded;
                movieDetails.ReleaseDate = movie.ReleaseDate;
                movieDetails.NumberInStock = movie.NumberInStock;



            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var movieList = _context.Movies.Where(m => m.Id == id).FirstOrDefault();
            MovieFormViewModel movieFormViewModel = new MovieFormViewModel()
            {
                Genres = _context.Genres.ToList(),
                Movie = movieList
            };
            return View("MovieForm", movieFormViewModel);
        }

        public ActionResult Details(int id)
        {
            var movieDetails = _context.Movies.Include(m => m.Genre).Where(m => m.Id == id).FirstOrDefault();
            return View(movieDetails);
        }

        public RedirectToRouteResult Delete(int id)
        {
            var movieDetails = _context.Movies.Where(m => m.Id == id).FirstOrDefault();
            _context.Movies.Remove(movieDetails);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}