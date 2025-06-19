using Microsoft.AspNetCore.Mvc;
using CinemaApp.Web.Models;
using CinemaApp.Web.Data;
using CinemaApp.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Web.Controllers
{
    public class CinemaController : Controller
    {
        private readonly CinemaAppDbContext _context;
        public CinemaController(CinemaAppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cinemas = _context.Cinemas.ToList();
            List<CinemaIndexViewModel> viewModel = cinemas.Select(c => new CinemaIndexViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location
            }).ToList();
            return View(viewModel);
        }
        public IActionResult Details(int Id)
        {
            var cinema = _context.Cinemas
                .Include(c => c.CinemaMovies)
                .ThenInclude(cm => cm.Movie)
                .FirstOrDefault(c => c.Id == Id);
            if (cinema == null)
            {
                return RedirectToAction("Index");
            }
            var viewModel = new CinemaDetailsViewModel
            {
                Id = cinema.Id,
                Name = cinema.Name,
                Location = cinema.Location,
                Movies = cinema.CinemaMovies
                .Select(cm => new MovieProgramViewModel()
                {
                    Title = cm.Movie.Title,
                    Duration = cm.Movie.Duration
                })
                .ToList()
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CinemaCreateViewModel());
        }
        [HttpPost]
        public IActionResult Create(CinemaCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var cinema = new Cinema
            {
                Name = vm.Name,
                Location = vm.Location
            };
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
