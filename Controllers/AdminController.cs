using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieTheater.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListMovies()
        {
            List <Movies> list = new List<Movies>();
            MovieDataAccess movies = new MovieDataAccess();
            list = movies.getAllMovies();
            return View(list);
        }

        [HttpGet]
        public ActionResult EditMovie( int id )
        {
            Movies TempMovie;
            MovieDataAccess movieData = new MovieDataAccess();
            TempMovie = movieData.FindMovie(id);
            return View(TempMovie);
        }


        [HttpPost]
        public ActionResult EditMovie(Movies TempMovie)
        {
            Movies Movie;
            MovieDataAccess movieData = new MovieDataAccess();
            movieData.EditMovie(TempMovie);
            return RedirectToAction("ListMovies");
        }

        [HttpGet]
        public ActionResult DeleteMovie ( int id )
        {
            Movies TempMovie;
            MovieDataAccess movieData = new MovieDataAccess();
            TempMovie = movieData.FindMovie(id);
            return View(TempMovie);

        }



        [HttpPost, ActionName("DeleteMovie")]
        public ActionResult DeleteMovieConfirm(int id)
        {
            MovieDataAccess tempmovie = new MovieDataAccess();
            tempmovie.DeleteMovie(id);
            return RedirectToAction("ListMovies");
        }


        [HttpGet]
        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMovie(Movies TempMovie)
        {
            if (ModelState.IsValid)
            {
                MovieDataAccess dataAccess = new MovieDataAccess();
                dataAccess.AddMovie(TempMovie);
                return RedirectToAction("ListMovies");
            }
            return View();  
        }
    }
}