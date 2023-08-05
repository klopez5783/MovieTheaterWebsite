using Antlr.Runtime.Misc;
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
using System.Drawing;

namespace MovieTheater.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult AdminButtons()
        {
            return View();
        }

        /*private ImageUtility _imageService = new ImageUtility();*/

        [HttpGet]
        public ActionResult ListMovies()
        {
            List<Movies> list = new List<Movies>();
            MovieDataAccess movies = new MovieDataAccess();
            list = movies.getAllMovies();
            return View("Movie/ListMovies", list);
        }

        [HttpGet]
        public ActionResult EditMovie(int id = 0 )
        {
            if (id == 0)
            {
                return RedirectToAction("ListMovies");
            }
            else
            {
                MovieView TempMovie = new MovieView();
                MovieDataAccess access = new MovieDataAccess();
                TempMovie = access.FindMovie(id);
                

                return View("Movie/EditMovie", TempMovie);
            }
            
        }

        [HttpPost]
        public ActionResult EditMovie(MovieView TempMovie)
        {

            
            MovieDataAccess access = new MovieDataAccess();
            try
            {
                ImageUtility.ProcessAndSaveImage(Request.Files, TempMovie.movie.MovieID, ModelState, access);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View("Movie/EditMovie", TempMovie);
            }
            access.EditMovie(TempMovie.movie);
            return RedirectToAction("ListMovies");
        }


        [HttpGet]
        public ActionResult MovieDetails(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("ListMovies");
            }
            else
            {
                MovieDataAccess access = new MovieDataAccess();
                MovieView movie = access.FindMovie(id);
                return View("Movie/MovieDetails", movie);
            }
        }

            [HttpGet]
        public ActionResult DeleteMovie(int id)
        {
            MovieDataAccess access = new MovieDataAccess();
            MovieView movie = access.FindMovie(id);
            return View("Movie/DeleteMovie");

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
            MovieView viewModel = new MovieView();
            return View("Movie/AddMovie", viewModel);

        }


        [HttpPost]
        public ActionResult AddMovie(MovieView TempMovie)
        {
            MovieDataAccess access = new MovieDataAccess();

            // Retrieve the generated MovieID
            int newMovieID = access.AddMovie(TempMovie.movie);

            try
            {
                ImageUtility.ProcessAndSaveImage(Request.Files, newMovieID, ModelState, access);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View("Movie/AddMovie", TempMovie);
            }

            return RedirectToAction("ListMovies");
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
        public ActionResult EditActor(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("ListActors");
            }
            else
            {
                ActorsDataAccess access =  new ActorsDataAccess();
                Actors actor;
                actor = access.FindActor(id);
                return View("Actors/EditActor",actor);
            }
            
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
        public ActionResult ActorDetails(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("ListActors");
            }
            else
            {
                ActorsDataAccess access = new ActorsDataAccess();
                Actors actor;
                actor = access.FindActor(id);
                return View("Actors/ActorDetails", actor);
            }
            
        }

        [HttpGet]
        public ActionResult DeleteActor(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("ListActors");
            }
            else
            {
                ActorsDataAccess access = new ActorsDataAccess();
                Actors actor;
                actor = access.FindActor(id);
                return View("Actors/DeleteActor", actor);
            }
            
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


        [HttpGet]
        public ActionResult ListDirectors()
        {
            List<Director> list = new List<Director>(); 
            DirectorDataAccess access = new DirectorDataAccess();
            list = access.getAllDirectors();
            return View("Directors/ListDirectors",list);
        }


        [HttpGet]
        public ActionResult AddDirector()
        {
            return View("Directors/AddDirector");
        }


        [HttpPost]
        public ActionResult AddDirector(Director director)
        {
            if (ModelState.IsValid)
            {
                if (director.ImageFile != null && director.ImageFile.ContentLength > 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(director.ImageFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(director.ImageFile.ContentLength);
                    }
                    director.DirectorIMG = imageData;
                }
                DirectorDataAccess access = new DirectorDataAccess();
                access.AddDirector(director);
                return RedirectToAction("ListDirectors");
            }
            return View("Directors/AddDirector");
        }

        [HttpGet]
        public ActionResult EditDirector(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("ListDirectors");
            }
            else
            {
                DirectorDataAccess data = new DirectorDataAccess();
                Director director = data.FindDirector(id);
                return View("Directors/EditDirector", director);
            }
        }


        [HttpPost]
        public ActionResult EditDirector(Director director)
        {
            if (ModelState.IsValid)
            {
                if (director.ImageFile != null && director.ImageFile.ContentLength > 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(director.ImageFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(director.ImageFile.ContentLength);
                    }
                    director.DirectorIMG = imageData;
                }
                else
                {
                    director.DirectorIMG = null;
                }
                DirectorDataAccess access=new DirectorDataAccess();
                access.EditDirector(director);
                return RedirectToAction("ListDirectors");
            }
            return View();
        }

        [HttpGet]
        public ActionResult DeleteDirector(int id = 0)
        {
            DirectorDataAccess data = new DirectorDataAccess();
            Director director = data.FindDirector(id);
            return View("Directors/DeleteDirector", director);
        }

        [HttpPost, ActionName("DeleteDirector")]
        public ActionResult DeleteDirectorConfirm(int id)
        {
            DirectorDataAccess access = new DirectorDataAccess();
            access.DeleteDirector(id);
            return RedirectToAction("ListDirectors");
        }

        [HttpGet]
        public ActionResult DirectorDetails(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("ListDirectors");
            }
            else
            {
            DirectorDataAccess data = new DirectorDataAccess();
            Director director = data.FindDirector(id);
            return View("Directors/DirectorDetails", director);
            }
            
        }

        public ActionResult GetDirectorIMG(int id)
        {
            DirectorDataAccess access = new DirectorDataAccess();
            byte[] imageData;
            imageData = access.GetDirectorImage(id);
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