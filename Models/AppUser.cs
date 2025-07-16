using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EventManagementSystem.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Address { get; set; }

        // Optional: Navigation property for events created by the user
        public ICollection<EventsTable>? Events { get; set; }
    }
}
