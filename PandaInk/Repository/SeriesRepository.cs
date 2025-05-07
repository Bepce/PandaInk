using Microsoft.EntityFrameworkCore;
using PandaInk.API.Data;
using PandaInk.API.DTOs.Seires;
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
        public async Task<IEnumerable<SeriesDTO>> GetAllSeriesAsync()
        {
             return await _context.Series
                .Select(s => s.ToSeriesDTO())
                .ToListAsync();
        }

        public async Task<SeriesDTO?> GetSeriesByIdAsync(Guid id)
        {
            return await _context.Series
                .Where(s => s.Id == id)
                .Select(s => s.ToSeriesDTO())
                .FirstOrDefaultAsync();
        }
    }
}
