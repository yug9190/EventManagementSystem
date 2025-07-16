using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Models;

namespace EventManagementSystem.Context
{
    public class CategoriesDbContext : DbContext
    {
        public CategoriesDbContext(DbContextOptions<CategoriesDbContext> options)
            : base(options)
        {
        }

        public DbSet<EventCategory> EventCategories { get; set; }
    }
}
