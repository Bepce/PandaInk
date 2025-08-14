using Microsoft.EntityFrameworkCore;
using PandaInk.API.Data;
using PandaInk.API.DTOs.Chapter;
using PandaInk.API.DTOs.Page;
using PandaInk.API.Interfaces;
using PandaInk.API.Models;

namespace PandaInk.API.Repository
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly PandaInkContext _context;
        public ChapterRepository(PandaInkContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Chapter chapter)
        {
            await _context.Chapters.AddAsync(chapter);
            await _context.SaveChangesAsync();
        }

        public async Task<ChapterDTO?> GetByIdAsync(Guid id)
        {
            var result = await _context.Chapters
                .Include(c => c.Content)
                .Where(c => c.Id == id)
                .OrderBy(c => c.Title)
                .Select(c => new ChapterDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    SeriesId = c.SeriesId,
                    Content = (List<Page?>)c.Content.OrderBy(c => c.PageNumber),
                })
                .FirstOrDefaultAsync();
                
            return result;
        }
    }
}
