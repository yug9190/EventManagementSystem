using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Models
{
    public class EventsTable
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        public string Status { get; set; }

       
    }
}
