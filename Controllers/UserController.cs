using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelTracker.Data;
using TravelTracker.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using TravelTracker.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == loginDto.Email);

            if (foundUser == null)
            {
                TempData["Error"] = "User not found";
                return RedirectToAction("Index", "Home", new { mode = "login" });
            }

            var hasher = new PasswordHasher<object>();
            var passwordsMatchResult = hasher.VerifyHashedPassword(null, foundUser.PasswordHash, loginDto.Password);


            if (passwordsMatchResult == PasswordVerificationResult.Failed)
            {
                TempData["Error"] = "Authentication failed.";
                return RedirectToAction("Index", "Home", new { mode = "login" });
            }

            TempData["Success"] = "Logged in successfuly.";
            return RedirectToAction("Index", "Map");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == registerDto.Email);

            if (foundUser != null)
            {
                TempData["Error"] = "User already exists";
                return RedirectToAction("Index", "Home", new { mode = "register" });
            }

            if (registerDto.Password != registerDto.RepeatPassword)
            {
                TempData["Error"] = "Passwords don't match";
                return RedirectToAction("Index", "Home", new { mode = "register" });
            }

            var hasher = new PasswordHasher<object>();
            string hashedPassword = hasher.HashPassword(null, registerDto.Password);

            var newUser = new User
            {
                Email = registerDto.Email,
                PasswordHash = hashedPassword
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Registered successfuly.";
            return RedirectToAction("Index", "Map");
        }
    }
}
