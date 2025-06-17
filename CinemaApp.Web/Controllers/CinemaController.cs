using Microsoft.AspNetCore.Mvc;
using CinemaApp.Web.Models;
using CinemaApp.Web.Data;
using CinemaApp.Web.ViewModels;

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
                Name = c.Name,
                Location = c.Location
            }).ToList();
            return View(viewModel);
        }
    }
}
