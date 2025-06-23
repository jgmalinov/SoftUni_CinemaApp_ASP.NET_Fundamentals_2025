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
                    Description = movieViewModel.Description,
                    ImageUrl = movieViewModel.ImageUrl
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

        [HttpGet]
        public IActionResult AddToProgram(int movieId)
        {
            var movie = _context.Movies.Find(movieId);
            if (movie is null)
            {
                return NotFound();
            }
            var cinemas = _context.Cinemas
                .Select(c => new CinemaCheckBoxItem()
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsChecked = false
                }).ToList();
            var viewModel = new AddMovieToCinemaProgramViewModel
            {
                MovieId = movie.Id,
                MovieTitle = movie.Title,
                Cinemas = cinemas
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult AddToProgram(AddMovieToCinemaProgramViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var existingAssignments = _context.CinemaMovies
                .Where(cm => cm.MovieId == vm.MovieId)
                .ToList();

            _context.RemoveRange(existingAssignments);

            foreach (var cinema in vm.Cinemas)
            {
                if (cinema.IsChecked)
                {
                    var cinemaMovie = new CinemaMovie()
                    {
                        MovieId = vm.MovieId,
                        CinemaId = cinema.Id
                    };
                    _context.CinemaMovies.Add(cinemaMovie);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
