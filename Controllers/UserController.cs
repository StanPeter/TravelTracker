using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelTracker.Data;
using TravelTracker.Models.DTOs;

namespace TravelTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register (RegisterDto registerDto)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
