using Microsoft.AspNetCore.Mvc;
using CinemaApp.Web.Models;
using CinemaApp.Web.Data;
using CinemaApp.Web.ViewModels;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly CinemaAppDbContext _context;
        public MovieController(CinemaAppDbContext context)
        {
            _context = context;
        }
        public static List<Movie> movies = new List<Movie>();

        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new MovieViewModel());
        }

        [HttpPost]
        public IActionResult Create(MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie()
                {
                    Title = movieViewModel.Title,
                    Genre = movieViewModel.Genre,
                    ReleaseDate = movieViewModel.ReleaseDate,
                    Director = movieViewModel.Director,
                    Duration = movieViewModel.Duration,
                    Description = movieViewModel.Description
                };
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieViewModel);
        }

        public IActionResult Details(int Id)
        {
            Movie? movie = _context.Movies.Find(Id);

            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }
    }
}
