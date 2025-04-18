using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelTracker.Models
{
    public class TripPlace
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public double Latitude { get; set; }
        [Required]

        public double Longitude { get; set; }
        public string? Description { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
