using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelTracker.Data;

namespace TravelTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        
        UserController(AppDbContext context)
        {
            _context = context;
        }


    }
}
