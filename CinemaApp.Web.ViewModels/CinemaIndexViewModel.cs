using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Web.ViewModels
{
    public class CinemaIndexViewModel
    {
        [Required(ErrorMessage="Cinema name is required")]
        [StringLength(50, ErrorMessage = "Cinema name cannot be longer than 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Cinema location is required")]
        [StringLength(100, ErrorMessage = "Cinema location is too long")]
        public string Location { get; set; } = null!;
    }
}
