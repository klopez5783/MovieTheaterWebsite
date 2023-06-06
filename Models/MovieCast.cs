using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater
{
    public class MovieCast
    {
        public int OldMovieID { get; set; } 

        public int OldActorID { get; set; } 

        public int MovieID { get; set; }

        public int ActorID { get; set; }

        public String Role {get; set; }
    }
}