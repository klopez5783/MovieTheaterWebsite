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

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        
        public DateTime Date { get; set; }


        [Required(ErrorMessage = "An Age Rating is required.")]
        public string AgeRating { get; set; }


        public int RunTime { get; set; }

        [Required(ErrorMessage = "A Movie Category is Required.")]
        public string Category { get; set; }


        [Required(ErrorMessage = "Movie Description is Required.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "The Language field is required.")]
        public string Language { get; set; }


        [Required(ErrorMessage = "Director ID is required.")]
        public int DirectorID { get; set; }

        public byte[] MovieIMG { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

    }
}