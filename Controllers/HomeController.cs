using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelTracker.Models;

namespace TravelTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string mode = "login", string? expired = null)
        {
            if (mode == "register")
            {
                ViewData["mode"] = "register";
            } else
            {
                ViewData["mode"] = "login";
            }

            if (expired != null)
                TempData["Error"] = "You were logged out due to inactivity.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}