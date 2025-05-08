using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PandaInk.API.Models;

namespace PandaInk.API.Data
{
    public class PandaInkContext : IdentityDbContext<ApplicationUser>
    {
        public PandaInkContext(DbContextOptions<PandaInkContext> options) : base(options)
        {
            
        }

        public DbSet<Series> Series { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            };

            modelBuilder.Entity<IdentityRole>()
                .HasData(roles);

            modelBuilder.Entity<Series>()
                .HasMany(s => s.Reviews)
                .WithOne(r => r.Series)
                .HasForeignKey(r => r.SeriesId);
        }
    }
}
