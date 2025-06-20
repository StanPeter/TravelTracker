﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelTracker.Data;
using TravelTracker.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using TravelTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using TravelTracker.Services;

namespace TravelTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;
        
        public UserController(AppDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == loginDto.Email);

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fix the form errors.";
                ViewData["mode"] = "login";
                return View("/Views/Home/Index.cshtml", loginDto);
            }

            if (foundUser == null)
            {
                TempData["Error"] = "Authentication failed. Please check your password and username.";
                ViewData["mode"] = "login";
                return View("/Views/Home/Index.cshtml", loginDto);
            }

            var hasher = new PasswordHasher<object>();
            var passwordsMatchResult = hasher.VerifyHashedPassword(null, foundUser.PasswordHash, loginDto.Password);


            if (passwordsMatchResult == PasswordVerificationResult.Failed)
            {
                TempData["Error"] = "Authentication failed. Please check your password and username.";
                ViewData["mode"] = "login";
                return View("/Views/Home/Index.cshtml", loginDto);
            }

            await _authService.SignInUserAsync(foundUser);

            TempData["Success"] = "Logged in successfuly.";
            return RedirectToAction("Index", "Map");
        }

        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == registerDto.Email);

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fix the form errors.";
                ViewData["mode"] = "register";
                return View("/Views/Home/Index.cshtml", registerDto);
            }

            if (foundUser != null)
            {
                TempData["Error"] = "User already exists.";
                ViewData["mode"] = "register";
                return View("/Views/Home/Index.cshtml", registerDto);
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

            await _authService.SignInUserAsync(newUser);

            TempData["Success"] = "Registered successfuly.";
            return RedirectToAction("Index", "Map");
        }
    }
}
