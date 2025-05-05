using Microsoft.EntityFrameworkCore;
using PandaInk.API.Models;

namespace PandaInk.API.Data
{
    public class PandaInkContext : DbContext
    {
        public PandaInkContext(DbContextOptions<PandaInkContext> options) : base(options)
        {

        }

        public DbSet<Series> Series { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Series>()
                .HasMany(s => s.Reviews)
                .WithOne(r => r.Series)
                .HasForeignKey(r => r.SeriesId);
        }
    }
}
