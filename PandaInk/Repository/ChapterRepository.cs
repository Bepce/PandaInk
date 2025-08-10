using Microsoft.EntityFrameworkCore;
using PandaInk.API.Data;
using PandaInk.API.DTOs.Chapter;
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
                .Where(c => c.Id == id)
                .Select(c => new ChapterDTO
                {
                    Title = c.Title,
                    Content = c.Content
                })
                .FirstOrDefaultAsync();
            return result;
        }
    }
}
