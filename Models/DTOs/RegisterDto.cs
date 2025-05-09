using System.ComponentModel.DataAnnotations;

namespace TravelTracker.Models.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; } = null!;
    }
}
