using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using TravelTracker.Models;

namespace TravelTracker.Services
{
    public class AuthService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task SignInUserAsync(User user)
        {
            // key pairs for authentication to attach cookie
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var identity = new ClaimsIdentity(claims, "MyAuthCookie");
            var principal = new ClaimsPrincipal(identity);

            await _contextAccessor.HttpContext!.SignInAsync("MyAuthCookie", principal);
        }
    }
}
