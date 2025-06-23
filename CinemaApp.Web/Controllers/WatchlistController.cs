using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CinemaApp.Web.Data;
using CinemaApp.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Web.Controllers
{
    public class WatchlistController : Controller
    {
        private readonly CinemaAppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public WatchlistController(CinemaAppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var watchListMovies = await _context.UserMovies
                .Where(um => um.UserId == _userManager.GetUserId(User))
                .Include(um => um.Movie)
                .Select(um => new WatchListViewModel()
                {
                    MovieId = um.Movie.Id,
                    Title = um.Movie.Title,
                    Genre = um.Movie.Genre,
                    ReleaseDate = um.Movie.ReleaseDate.ToString("MMMM yyyy"),
                    ImageUrl = um.Movie.ImageUrl
                })
                .ToListAsync();

            return View(watchListMovies);
        }

        public async Task<IActionResult> AddToWatchlist(int movieId)
        {
            var userId = _userManager.GetUserId(User);
            var userMovie = await _context.UserMovies
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);
            if (userMovie == null)
            {
                userMovie = new Models.UserMovie
                {
                    UserId = userId,
                    MovieId = movieId
                };
                _context.UserMovies.Add(userMovie);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Movie");
        }

        public async Task<IActionResult> RemoveFromWatchList(int movieId)
        {
            var userId = _userManager.GetUserId(User);
            var userMovie = await _context.UserMovies
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);
            
            if (userMovie != null)
            {
                _context.UserMovies.Remove(userMovie);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Watchlist");
        }
    }
}
