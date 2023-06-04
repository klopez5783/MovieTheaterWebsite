using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater
{
    public class MovieCast
    {
        public int MovieID { get; set; }

        public int ActorID { get; set; }

        public String Role {get; set; }
    }
}