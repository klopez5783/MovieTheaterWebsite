using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieTheater
{
    public class MovieImages
    {

        public int ImageID { get; set; }

        public int MovieID { get; set; }

        public byte[] MovieIMG { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

    }
}