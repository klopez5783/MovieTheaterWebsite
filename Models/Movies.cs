using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MovieTheater
{
    public class Movies
    {
        public int MovieID { get; set; }
        public string Name { get; set; }

        
        public DateTime Date { get; set; }

        public string AgeRating { get; set; }

        public int RunTime { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public int DirectorID { get; set; }

        public byte[] MovieIMG { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

    }
}