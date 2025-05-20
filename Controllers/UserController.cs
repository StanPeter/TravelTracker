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
                return StatusCode(500, "User does not exist.");
            }

            var hasher = new PasswordHasher<object>();
            var passwordsMatchResult = hasher.VerifyHashedPassword(null, foundUser.PasswordHash, loginDto.Password);


            if (passwordsMatchResult == PasswordVerificationResult.Failed)
            {
                return StatusCode(500, "Authentication failed.");
            }

            return RedirectToAction("Index", "Map");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == registerDto.Email);

            if (foundUser != null)
            {
                return BadRequest(new {error = "User already exists" });
            }

            if (registerDto.Password != registerDto.RepeatPassword)
            {
                return BadRequest(new { error = "Passwords don't match"});
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

            return RedirectToAction("Index", "Map");
        }
    }
}
