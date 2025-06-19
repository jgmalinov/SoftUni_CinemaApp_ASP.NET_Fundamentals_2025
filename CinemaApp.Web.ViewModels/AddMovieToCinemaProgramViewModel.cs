using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Web.ViewModels
{
    public class AddMovieToCinemaProgramViewModel
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = null!;
        public List<CinemaCheckBoxItem> Cinemas { get; set; } = new List<CinemaCheckBoxItem>();
    }
}
