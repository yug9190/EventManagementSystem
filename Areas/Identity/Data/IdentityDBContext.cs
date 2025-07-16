using EventManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Areas.Identity.Data
{
    public class IdentityDBContext : IdentityDbContext<AppUser>
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Rename ASP.NET Identity tables (optional, but clearer)
            builder.Entity<AppUser>(entity => entity.ToTable("Users"));
            builder.Entity<IdentityRole>(entity => entity.ToTable("Roles"));
            builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable("UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("UserClaims"));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("UserLogins"));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("RoleClaims"));
            builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserTokens"));

            // Additional configurations (optional)
            builder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.Name)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.Address)
                      .HasMaxLength(250);
            });
        }
    }
}
