using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Web.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Cinema name cannot be longer than 100 characters")]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; } = null!;
        public ICollection<CinemaMovie> CinemaMovies { get; set; } = new List<CinemaMovie>();

    }
}
