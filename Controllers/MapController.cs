using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelTracker.Data;
using TravelTracker.Models;

namespace TravelTracker.Controllers
{
    [Authorize]
    public class MapController: Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public MapController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IActionResult Index()
        {
            List<TripPlace> tripPlaces = _context.TripPlaces.Include(t => t.User).ToList();

            ViewBag.GoogleMapsApiKey = _config["GoogleMaps:Apikey"];

            return View(tripPlaces);
        }
    }
}
