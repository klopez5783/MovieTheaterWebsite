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

        public string GetRunTime()
        {
            var hours = RunTime / 60;
            var minutes = RunTime % 60;

            if (minutes == 0)
            {
                return $"{hours}HR";
            }

            return $"{hours} HR {minutes} MIN";


        }

        [Required(ErrorMessage = "A Movie Category is Required.")]
        public string Category { get; set; }


        [Required(ErrorMessage = "Movie Description is Required.")]
        public string Description { get; set; }


        [MaxLength(5000), Required(ErrorMessage = "The Language field is required.")]
        public string Language { get; set; }


        [Required(ErrorMessage = "Director ID is required.")]
        public int DirectorID { get; set; }

    }
}