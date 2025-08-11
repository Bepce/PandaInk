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

        public async Task<List<ChapterDTO?>> GetByIdAsync(Guid id)
        {
            var result = await _context.Chapters
                .Include(c => c.Content)
                .Where(c => c.SeriesId == id)
                .Select(c => new ChapterDTO
                {
                    Title = c.Title,
                 
                })
                .ToListAsync();
            return result;
        }

        public async Task<PageDTO?> GetPageByPageNumber(int pageNumber, Guid chapterId)
        {
            var result = await _context.Pages
                .Where(p => p.ChapterId == chapterId && pageNumber == pageNumber)
                .Select(p => new PageDTO
                {

                    PageNumber = p.PageNumber,
                    ImageUrl = p.ImageUrl
                }).FirstOrDefaultAsync();

                return result;
        }
    }
}
