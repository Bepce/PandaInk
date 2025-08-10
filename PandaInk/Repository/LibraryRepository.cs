using Microsoft.EntityFrameworkCore;
using PandaInk.API.Data;
using PandaInk.API.Interfaces;
using PandaInk.API.Models;

namespace PandaInk.API.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly PandaInkContext _context;
        public LibraryRepository(PandaInkContext context)
        {
            _context = context;
        }

        public async Task<List<Series>> GetUserLibraryAsync(ApplicationUser user)
        {
            return await _context.Libraries
                .Where(l => l.UserId == user.Id)
                .Select(series => new Series
                {
                    Id = series.SeriesId,
                    Title = series.Series.Title,
                    Description = series.Series.Description,
                    CoverImage = series.Series.CoverImage,
                    Author = series.Series.Author,
                    Genre = series.Series.Genre,
                    ReleaseDate = series.Series.ReleaseDate
                }).ToListAsync();
        }
    }
}
