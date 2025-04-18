﻿using System.ComponentModel.DataAnnotations;

namespace TravelTracker.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string UserName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;

        public string? Name { get; set; }
        public string? Surname { get; set; }

        public ICollection<TripPlace> TripPlaces { get; set; } = new List<TripPlace>();
    }
}
