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

            modelBuilder.Entity<Series>()
                .HasMany(s => s.Reviews)
                .WithOne(r => r.Series)
                .HasForeignKey(r => r.SeriesId);

            modelBuilder.Entity<Series>()
                .HasMany(s => s.Chapters)
                .WithOne(c => c.Series)
                .HasForeignKey(c => c.SeriesId);

            modelBuilder.Entity<Chapter>()
                .HasMany(c => c.Content)
                .WithOne(p => p.Chapter)
                .HasForeignKey(p => p.ChapterId);

            SeedUser(modelBuilder);
            SeedSeries(modelBuilder);
            SeedChapters(modelBuilder);
            SeedPages(modelBuilder);
        }

        private void SeedUser(ModelBuilder modelBuilder)
        {
            var admin = new ApplicationUser
            {
                Id = "f9f9694c-3118-481c-81b7-eba2a9123f91",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Admin123"),
                Email = "admin@admin.com"
            };

            var user = new ApplicationUser
            {
                Id ="3bf4ffff-5c78-4a62-b687-609b3cc0b6a6",
                UserName = "user",
                NormalizedUserName = "USER",
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "User123"),
                Email = "user@user.com"
            };



            var adminRole = new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            var userRole = new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            };

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = admin.Id,
                    RoleId = adminRole.Id
                },
                new IdentityUserRole<string>
                {
                    UserId = user.Id,
                    RoleId = userRole.Id
                }
                );

            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    adminRole,
                    userRole
                );

            modelBuilder.Entity<ApplicationUser>().HasData(
                admin,
                user
                );


        }

        private void SeedSeries(ModelBuilder modelBuilder)
        {
            var naruto = new Series
            {
                Id = Guid.Parse("965c43c5-cc22-496b-8abb-001f9eadda7d"),
                Title = "Naruto",
                Description = "Naruto is a Japanese manga series written and illustrated by Masashi Kishimoto. It tells the story of Naruto Uzumaki, a young ninja who seeks recognition from his peers and dreams of becoming the Hokage, the leader of his village.",
                CoverImage = "https://upload.wikimedia.org/wikipedia/en/9/94/NarutoCoverTankobon1.jpg",
                Author = "Masashi Kishimoto",
                Genre = "Fantasy",
                ReleaseDate = new DateTime(1999, 9, 21)
            };

            var oncPiece = new Series
            {
                Id = Guid.Parse("e31d4a9c-8323-4e58-9513-90af704b1ada"),
                Title = "One Piece",
                Description = "One Piece is a Japanese manga series written and illustrated by Eiichiro Oda. It follows the adventures of Monkey D. Luffy and his pirate crew in their quest to find the One Piece, the greatest treasure in the world.",
                CoverImage = "https://upload.wikimedia.org/wikipedia/en/9/90/One_Piece%2C_Volume_61_Cover_%28Japanese%29.jpg",
                Author = "Eiichiro Oda",
                Genre = "Adventure",
                ReleaseDate = new DateTime(1997, 7, 22)
            };
            modelBuilder.Entity<Series>().HasData(
                naruto,
                oncPiece
                );
        }

        private void SeedChapters(ModelBuilder modelBuilder)
        {
            var narutoSeriesId = Guid.Parse("965c43c5-cc22-496b-8abb-001f9eadda7d");
            var onePieceSeriesId = Guid.Parse("e31d4a9c-8323-4e58-9513-90af704b1ada");


            var narutoChapter1 = new Chapter
            {
                Id = Guid.Parse("577f2944-cec7-4b21-aaee-f794c1cf6381"),
                Title = "Chapter 1: Naruto Uzumaki",
                ChapterNumber = 1,
                SeriesId = narutoSeriesId,
            };
            var narutoChapter2 = new Chapter
            {
                Id = Guid.Parse("9ca453a0-ed51-4e9e-96fb-1b3de3a3a04b"),
                Title = "Chapter 2: The Worst Client",
                ChapterNumber = 2,
                SeriesId = narutoSeriesId,
            };

            var onePieceChapter1 = new Chapter
            {
                Id = Guid.Parse("cccd4420-5e1a-4630-ab20-6e0ef2225ee9"),
                Title = "Chapter 1: Romance Dawn",
                ChapterNumber = 1,
                SeriesId = onePieceSeriesId,
            };

            var onePieceChapter2 = new Chapter
            {
                Id = Guid.Parse("e115de06-3c25-4e3c-9a01-53ce220e8c17"),
                Title = "Chapter 2: They Call Him \"Straw Hat Luffy\"",
                ChapterNumber = 2,
                SeriesId = onePieceSeriesId,
            };

            modelBuilder.Entity<Chapter>().HasData(
                narutoChapter1,
                narutoChapter2,
                onePieceChapter1,
                onePieceChapter2
            );
        }

        private void SeedPages(ModelBuilder modelBuilder)
        {
            var narutoChapter1Id = Guid.Parse("577f2944-cec7-4b21-aaee-f794c1cf6381");

            var onePieceChapter1Id = Guid.Parse("cccd4420-5e1a-4630-ab20-6e0ef2225ee9");



            var narutoPage1 = new Page
            {
                Id = Guid.NewGuid(),
                ImageUrl = "https://cmsapi-frontend.naruto-official.com/site/api/naruto/Image/get?path=/naruto/en/comics/2022/09/29/E7fnRJ3vSgYHriRY/2.jpg",
                PageNumber = 1,
                ChapterId = narutoChapter1Id,
            };

            var narutoPage2 = new Page
            {
                Id = Guid.NewGuid(),
                ImageUrl = "https://cmsapi-frontend.naruto-official.com/site/api/naruto/Image/get?path=/naruto/en/comics/2022/09/29/4Oo1qlwVNnr1BffM/3.jpg",
                PageNumber = 2,
                ChapterId = narutoChapter1Id,
            };

            var narutoPage3 = new Page
            {
                Id = Guid.NewGuid(),
                ImageUrl = "https://cmsapi-frontend.naruto-official.com/site/api/naruto/Image/get?path=/naruto/en/comics/2022/09/29/ubq1tyQ5SG3QNxww/1.jpg",
                PageNumber = 3,
                ChapterId = narutoChapter1Id,
            };

            var onePiecePage1 = new Page
            {
                Id = Guid.NewGuid(),
                ImageUrl = "https://eu2.contabostorage.com/2352a0b47a16442aa2bd93b0a47735ea:manga/1piece/Chapter%201/01.jpg",
                PageNumber = 1,
                ChapterId = onePieceChapter1Id,
            };

            var onePiecePage2 = new Page
            {
                Id = Guid.NewGuid(),
                ImageUrl = "https://eu2.contabostorage.com/2352a0b47a16442aa2bd93b0a47735ea:manga/1piece/Chapter%201/02.jpg",
                PageNumber = 2,
                ChapterId = onePieceChapter1Id,
            };

            var onePiecePage3 = new Page
            {
                Id = Guid.NewGuid(),
                ImageUrl = "https://eu2.contabostorage.com/2352a0b47a16442aa2bd93b0a47735ea:manga/1piece/Chapter%201/03.jpg",
                PageNumber = 3,
                ChapterId = onePieceChapter1Id,
            };

            modelBuilder.Entity<Page>().HasData(
                narutoPage1,
                narutoPage2,
                narutoPage3,
                onePiecePage1,
                onePiecePage2,
                onePiecePage3
            );
        }
    }
}
