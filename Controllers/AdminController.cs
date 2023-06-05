using MovieTheater.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
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
            List<Movies> list = new List<Movies>();
            MovieDataAccess movies = new MovieDataAccess();
            list = movies.getAllMovies();
            return View("Movie/ListMovies", list);
        }

        [HttpGet]
        public ActionResult EditMovie(int id)
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
            else
            {
                TempMovie.MovieIMG = null;
            }
            MovieDataAccess movieData = new MovieDataAccess();
            movieData.EditMovie(TempMovie);
            return RedirectToAction("ListMovies");
        }


        [HttpGet]
        public ActionResult MovieDetails(int id)
        {
            Movies tempMovie;
            MovieDataAccess access = new MovieDataAccess();
            tempMovie = access.FindMovie(id);
            return View("Movie/MovieDetails", tempMovie);
        }

        [HttpGet]
        public ActionResult DeleteMovie(int id)
        {
            Movies TempMovie;
            MovieDataAccess movieData = new MovieDataAccess();
            TempMovie = movieData.FindMovie(id);
            return View("Movie/DeleteMovie", TempMovie);

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
            imageData = access.GetMovieImage(id);
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


        public ActionResult ListActors()
        {
            List<Actors> list = new List<Actors>();
            ActorsDataAccess access = new ActorsDataAccess();
            list = access.ListActors();
            return View("Actors/ListActors", list);
        }

        [HttpGet]
        public ActionResult AddActor()
        {
            return View("Actors/AddActor");
        }

        [HttpPost]
        public ActionResult AddActor(Actors actor)
        {
            if (ModelState.IsValid)
            {
                if (actor.ImageFile != null && actor.ImageFile.ContentLength > 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(actor.ImageFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(actor.ImageFile.ContentLength);
                    }
                    actor.ActorIMG = imageData;
                }
                ActorsDataAccess access = new ActorsDataAccess();
                access.AddActor(actor);
                return RedirectToAction("ListActors");
            }
            return View();
            
        }



        [HttpGet]
        public ActionResult EditActor(int id)
        {
            ActorsDataAccess access =  new ActorsDataAccess();
            Actors actor;
            actor = access.FindActor(id);
            return View("Actors/EditActor",actor);
        }


        [HttpPost]
        public ActionResult EditActor(Actors actors)
        {
            if (actors.ImageFile != null && actors.ImageFile.ContentLength > 0)
            {
                byte[] imageData;
                using (var binaryReader = new BinaryReader(actors.ImageFile.InputStream))
                {
                    imageData = binaryReader.ReadBytes(actors.ImageFile.ContentLength);
                }
                actors.ActorIMG = imageData;
            }
            else
            {
                actors.ActorIMG = null;
            }
            ActorsDataAccess access = new ActorsDataAccess();
            access.EditMovie(actors);
            return RedirectToAction("ListActors");
        }

        [HttpGet]
        public ActionResult ActorDetails(int id)
        {
            ActorsDataAccess access = new ActorsDataAccess();
            Actors actor;
            actor = access.FindActor(id);
            return View("Actors/ActorDetails", actor);
        }

        [HttpGet]
        public ActionResult DeleteActor(int id)
        {
            ActorsDataAccess access = new ActorsDataAccess();
            Actors actor;
            actor = access.FindActor(id);
            return View("Actors/DeleteActor", actor);
        }

        [HttpPost, ActionName("DeleteActor")]
        public ActionResult DeleteActorConfirm(int id)
        {
            ActorsDataAccess actor = new ActorsDataAccess();
            actor.DeleteActor(id);
            return RedirectToAction("ListActors");
        }


        public ActionResult GetActorIMG(int id)
        {
            ActorsDataAccess access = new ActorsDataAccess();
            byte[] imageData;
            imageData = access.GetActorImage(id);
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


        public ActionResult ListMovieCast()
        {
            List<MovieCast> list = new List<MovieCast>();
            MovieCastDataAccess access = new MovieCastDataAccess();
            list = access.getAllMoviesCast();
            return View("MovieCast/ListMovieCast",list);
        }

        [HttpGet]
        public ActionResult AddMovieCast()
        {
            return View("MovieCast/AddMovieCast");
        }


        [HttpPost]
        public ActionResult AddMovieCast(MovieCast TempMovie)
        {
            if (ModelState.IsValid)
            {
                MovieCastDataAccess dataAccess = new MovieCastDataAccess();
                dataAccess.AddMovieCast(TempMovie);
                return RedirectToAction("ListMovieCast");
            }
            return View();
        }


        [HttpGet]
        public ActionResult EditMovieCast(int MovieID, int ActorID )
        {
            MovieCast movieCast = new MovieCast();
            MovieCastDataAccess access = new MovieCastDataAccess();
            movieCast = access.FindMovieCast(MovieID, ActorID);
            movieCast.OldMovieID = MovieID;
            movieCast.OldActorID = ActorID; 
            return View("MovieCast/EditMovieCast", movieCast);
        }

        [HttpPost]
        public ActionResult EditMovieCast(MovieCast MovieCast)
        {
            if (ModelState.IsValid)
            {
                MovieCastDataAccess access = new MovieCastDataAccess();
                access.EditMovieCast(MovieCast);
                return RedirectToAction("ListMovieCast");
            }
            return View();
        }


        [HttpGet]
        public ActionResult DeleteMovieCast(int MovieID, int ActorID)
        {
            MovieCast movieCast = new MovieCast();
            MovieCastDataAccess access = new MovieCastDataAccess();
            movieCast = access.FindMovieCast(MovieID,ActorID);
            return View("MovieCast/DeleteMovieCast", movieCast);
        }

        [HttpPost, ActionName("DeleteMovieCast")]
        public ActionResult DeleteMovieCastConfirm(int MovieID, int ActorID)
        {
            MovieCast movieCast = new MovieCast();
            MovieCastDataAccess access = new MovieCastDataAccess();
            access.DeleteMovieCast(MovieID, ActorID);
            return RedirectToAction("ListMovieCast");
        }

        [HttpGet]
        public ActionResult MovieCastDetails(int MovieID, int ActorID)
        {
            MovieCast movieCast = new MovieCast();
            MovieCastDataAccess access = new MovieCastDataAccess();
            movieCast = access.FindMovieCast(MovieID, ActorID);
            return View("MovieCast/MovieCastDetails", movieCast);
        }

    }

}