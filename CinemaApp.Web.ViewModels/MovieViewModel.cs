using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Web.ViewModels
{
    public class MovieViewModel
    {
        public MovieViewModel()
        {
            ReleaseDate = DateTime.Today;
        }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; } = null!;
        [Required(ErrorMessage = "Release date is required")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Director name is required")]
        [StringLength(100, ErrorMessage = "Director name cannot be longer than 100 characters")]
        public string Director { get; set; } = null!;
        [Range(1, 500, ErrorMessage = "Duration must be between 1 and 500 minutes")]
        public int Duration { get; set; }
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
