using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PandaInk.API.Data;
using PandaInk.API.DTOs.Seires;
using PandaInk.API.Helpers;
using PandaInk.API.Interfaces;
using PandaInk.API.Mappers;
using PandaInk.API.Models;

namespace PandaInk.API.Repository
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly PandaInkContext _context;
        public SeriesRepository(PandaInkContext context)
        {
            _context = context;
        }

        public Task<Series> AddSeries(SeriesDTO series)
        {
            var newSeries = new Series
            {
                Title = series.Title,
                Description = series.Description,
                ReleaseDate = DateTime.Parse(series.ReleaseDate),
                Genre = series.Genre,
                CoverImage = series.CoverImage,
                Author = series.Author,
                Reviews = new List<Review>(),
                Chapters = new List<Chapter>()
            };
            
            _context.Series.Add(newSeries);
            _context.SaveChangesAsync();

            return Task.FromResult(newSeries);
        }

        public async Task<IEnumerable<SeriesCardDTO>> GetAllSeriesAsync(QueryObject query)
        {
            var series = _context.Series
                .Include(s => s.Reviews)               
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.Title))
            {
                series = series.Where(s => s.Title.ToLower().Contains(query.Title.ToLower()));
            }

            if(!string.IsNullOrEmpty(query.SortBy))
            {
                if (query.SortBy.Equals("ReleaseDate", StringComparison.OrdinalIgnoreCase))
                {
                    series = query.IsDescending ? series.OrderByDescending(s => s.ReleaseDate) : series.OrderBy(s => s.ReleaseDate);
                }
            }

            return await series.Select(s => s.ToSeriesCardDTO()).ToListAsync();
        }

        public async Task<SeriesDTO?> GetSeriesByIdAsync(Guid id)
        {
            return await _context.Series
                .Where(s => s.Id == id)
                .Include(s => s.Reviews)
                .Include(s => s.Chapters)
                .Select(s => s.ToSeriesDTO())
                .FirstOrDefaultAsync();
        }

        public Task<bool> SeriesExsistByName(string name)
        {
            return _context.Series
                .AnyAsync(s => s.Title.ToLower() == name.ToLower());
        }
    }
}
