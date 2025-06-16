using Microsoft.AspNetCore.Mvc;
using CinemaApp.Web.Models;
using CinemaApp.Web.Data;

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
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
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
