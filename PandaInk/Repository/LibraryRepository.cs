using Microsoft.EntityFrameworkCore;
using PandaInk.API.Data;
using PandaInk.API.DTOs.Seires;
using PandaInk.API.Interfaces;
using PandaInk.API.Mappers;
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

        public async Task AddToLibraryAsync(Library libraryEntry)
        {
            await _context.Libraries.AddAsync(libraryEntry);
            await _context.SaveChangesAsync();           
        }

        public async Task<List<SeriesDTO>> GetUserLibraryAsync(ApplicationUser user)
        {
            return await _context.Libraries
                .Where(l => l.UserId == user.Id)
                .Include(l => l.Series.Chapters)
                .Select(l => l.Series.ToSeriesDTO())
                .ToListAsync();
        }

        public async Task<bool> LibraryEntryExistsAsync(Library libraryEntry)
        {
             return _context.Libraries.Any(l => l.UserId == libraryEntry.UserId && l.SeriesId == libraryEntry.SeriesId);
        }

        public async void RemoveFromLibrary(Library libraryEntry)
        {
            await _context.Libraries
                .Where(l => l.UserId == libraryEntry.UserId && l.SeriesId == libraryEntry.SeriesId)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
    }
}
