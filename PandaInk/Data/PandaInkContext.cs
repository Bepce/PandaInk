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
        public DbSet<Library> Libraries { get; set; } = null!;
        public DbSet<Chapter> Chapters { get; set; } = null!;
        public DbSet<Page> Pages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Library>()
                .HasKey(l => new { l.UserId, l.SeriesId });

            modelBuilder.Entity<Library>()
                .HasOne(u => u.User)
                .WithMany(u => u.Libraries)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<Library>()
                .HasOne(s => s.Series)
                .WithMany(s => s.Libraries)
                .HasForeignKey(l => l.SeriesId);

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

            modelBuilder.Entity<Series>()
                .HasMany(s => s.Chapters)
                .WithOne(c => c.Series)
                .HasForeignKey(c => c.SeriesId);
        }
    }
}
