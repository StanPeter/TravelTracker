using Microsoft.EntityFrameworkCore;
using TravelTracker.Models;

namespace TravelTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TripPlace> TripPlaces { get; set;}
    }
}
