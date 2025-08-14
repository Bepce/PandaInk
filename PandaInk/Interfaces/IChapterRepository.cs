using PandaInk.API.DTOs.Chapter;
using PandaInk.API.DTOs.Page;
using PandaInk.API.Models;

namespace PandaInk.API.Interfaces
{
    public interface IChapterRepository
    {
        Task CreateAsync(Chapter chapter);
        Task<ChapterDTO?> GetByIdAsync(Guid chapterId);
        Task<PageDTO> GetPageByPageNumber(int? pageNumber, Guid chapterId);
    }
}
