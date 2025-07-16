using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Models;

namespace EventManagementSystem.Context
{
    public class EventsDbContext : DbContext
    {
        public EventsDbContext(DbContextOptions<EventsDbContext> options)
            : base(options)
        {
        }

        public DbSet<EventsTable> EventsTable { get; set; }
        public DbSet<Booking> Bookings { get; set; }  // Add this
    }
}
