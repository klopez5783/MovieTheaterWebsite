﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieTheater.Controllers
{
    public class Admin : Controller
    {
        public ActionResult index()
        {
            return View();
        }

        public ActionResult ListMovies()
        {
            List <Movies> list = new List<Movies>();
            MovieDataAccess movies = new MovieDataAccess();
            list = movies.getAllMovies();
            return View(list);
        }
    }
}