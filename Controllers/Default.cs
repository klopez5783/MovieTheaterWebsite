using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieTheater.Controllers
{
    public class Default : Controller
    {
        public ActionResult index()
        {
            return View();
        }
    }
}