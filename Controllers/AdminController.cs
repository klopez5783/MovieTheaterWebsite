using MovieTheater.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mime;
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
            return View("Movie/ListMovies",list);
        }

        [HttpGet]
        public ActionResult EditMovie( int id )
        {
            Movies TempMovie;
            MovieDataAccess movieData = new MovieDataAccess();
            TempMovie = movieData.FindMovie(id);
            return View("Movie/EditMovie", TempMovie);
        }

        [HttpPost]
        public ActionResult EditMovie(Movies TempMovie)
        {
            if (TempMovie.ImageFile != null && TempMovie.ImageFile.ContentLength > 0)
            {
                byte[] imageData;
                using (var binaryReader = new BinaryReader(TempMovie.ImageFile.InputStream))
                {
                    imageData = binaryReader.ReadBytes(TempMovie.ImageFile.ContentLength);
                }
                TempMovie.MovieIMG = imageData;
            }
            MovieDataAccess movieData = new MovieDataAccess();
            movieData.EditMovie(TempMovie);
            return RedirectToAction("Movie/ListMovies");
        }


        [HttpGet]
        public ActionResult MovieDetails(int id)
        {
            Movies tempMovie;
            MovieDataAccess access = new MovieDataAccess();
            tempMovie = access.FindMovie(id);
            return View("Movie/MovieDetails" , tempMovie);
        }

        [HttpGet]
        public ActionResult DeleteMovie ( int id )
        {
            Movies TempMovie;
            MovieDataAccess movieData = new MovieDataAccess();
            TempMovie = movieData.FindMovie(id);
            return View("Movie/DeleteMovie" , TempMovie);

        }



        [HttpPost, ActionName("DeleteMovie")]
        public ActionResult DeleteMovieConfirm(int id)
        {
            MovieDataAccess tempmovie = new MovieDataAccess();
            tempmovie.DeleteMovie(id);
            return RedirectToAction("Movie/ListMovies");
        }


        [HttpGet]
        public ActionResult AddMovie()
        {
            return View("Movie/AddMovie");
        }


        [HttpPost]
        public ActionResult AddMovie(Movies TempMovie)
        {
            if (ModelState.IsValid)
            {
                if (TempMovie.ImageFile != null && TempMovie.ImageFile.ContentLength > 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(TempMovie.ImageFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(TempMovie.ImageFile.ContentLength);
                    }
                    TempMovie.MovieIMG = imageData;
                }

                MovieDataAccess dataAccess = new MovieDataAccess();
                dataAccess.AddMovie(TempMovie);
                return RedirectToAction("ListMovies");
            }
            return View();  
        }



        public ActionResult GetMovieIMG(int id)
        {
            MovieDataAccess access = new MovieDataAccess();
            byte[] imageData;
            imageData = access.GetImage(id);
            if (imageData != null)
            {
                string imageType = ImageUtility.GetImageType(imageData);
                var result = new FileContentResult(imageData, imageType);

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}