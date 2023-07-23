using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieTheater
{
    public class Director
    {
        public int DirectorID { get; set; } 

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] DirectorIMG { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

    }
}