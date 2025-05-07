using Microsoft.EntityFrameworkCore;
using PandaInk.API.Data;
using PandaInk.API.DTOs.Seires;
using PandaInk.API.Helpers;
using PandaInk.API.Interfaces;
using PandaInk.API.Mappers;

namespace PandaInk.API.Repository
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly PandaInkContext _context;
        public SeriesRepository(PandaInkContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SeriesDTO>> GetAllSeriesAsync(QueryObject query)
        {
            var series = _context.Series
                .Include(s => s.Reviews)               
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.Title))
            {
                series = series.Where(s => s.Title.ToLower().Contains(query.Title.ToLower()));
            }

            return await series.Select(s => s.ToSeriesDTO()).ToListAsync();
        }

        public async Task<SeriesDTO?> GetSeriesByIdAsync(Guid id)
        {
            return await _context.Series
                .Where(s => s.Id == id)
                .Include(s => s.Reviews)
                .Select(s => s.ToSeriesDTO())
                .FirstOrDefaultAsync();
        }
    }
}
