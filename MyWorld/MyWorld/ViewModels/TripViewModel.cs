using System;
using System.ComponentModel.DataAnnotations;

namespace MyWorld.ViewModels
{
    public class TripViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}