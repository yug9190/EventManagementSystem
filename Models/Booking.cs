using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public EventsTable Event { get; set; }

        public string AppUserId { get; set; }

        public string Status { get; set; } = "Booked";

        public DateTime BookingDate { get; set; } = DateTime.Now;
    }
}
