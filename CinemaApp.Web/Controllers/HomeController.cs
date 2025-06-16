using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.Message = "Welcome to the Cinema App!";
            return View();
        }
    }
}
