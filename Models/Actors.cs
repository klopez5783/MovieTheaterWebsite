using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieTheater
{
    public class Actors
    {
        public int ActorID { get; set; }

        [Required(ErrorMessage = "Actors First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Actos Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        public byte[] ActorIMG { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}