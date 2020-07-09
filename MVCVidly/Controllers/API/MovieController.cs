using AutoMapper;
using MVCVidly.Dtos;
using MVCVidly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace MVCVidly.Controllers.API
{
    public class MovieController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //Get/api/Movie
        //public IEnumerable<Movie> GetMovies()
        //{
        //    return _context.Movies.ToList();
        //}
        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            //return _context.Movies.ToList().Select(Mapper.Map<Movie,MovieDto>);
            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            return moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
        }

        //Get/api/Movie/1
        //public Movie GetMovie(int id)
        //{
        //    var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
        //    if (movie == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    return movie;
        //}

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            //return Mapper.Map<Movie,MovieDto>(movie);
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //Post/api/Movie
        [HttpPost]
        //public Movie CreateMovie(Movie movie)
        //{
        //    if (movie == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    _context.Movies.Add(movie);
        //    _context.SaveChanges();
        //    return movie;

        //}
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (movieDto == null)
                // throw new HttpResponseException(HttpStatusCode.NotFound);
                return BadRequest();
            if (!ModelState.IsValid)
                // throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            movieDto.Id = movie.Id;
            //return movieDto;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);

        }

        //Put/api/Movie/1

        [HttpPut]
        //public void UpdateMovie(int id,Movie movie)
        //{
        //    if (movie == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    var movieInDb = _context.Movies.FirstOrDefault(m => m.Id == id);
        //    movieInDb.Name = movie.Name;
        //    movieInDb.GenreId = movie.GenreId;
        //    movieInDb.DateAdded = movie.DateAdded;
        //    movieInDb.ReleaseDate = movie.ReleaseDate;
        //    movieInDb.NumberInStock = movie.NumberInStock;
        //    _context.SaveChanges();
        //}
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (movieDto == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var movieInDb = _context.Movies.FirstOrDefault(m => m.Id == id);
            Mapper.Map(movieDto, movieInDb);
            _context.SaveChanges();
        }

        //Delete/api/Movie/1
        [HttpDelete]

        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.FirstOrDefault(m => m.Id == id);
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }
    }

    
}
