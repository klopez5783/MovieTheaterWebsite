using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieTheater
{
    public class MovieView
    {
        public Movies movie { get; set; }

        public List<MovieImages> images { get; set; }
    }
}