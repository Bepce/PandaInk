using PandaInk.API.DTOs.Chapter;
using PandaInk.API.DTOs.Page;
using PandaInk.API.Models;

namespace PandaInk.API.Interfaces
{
    public interface IChapterRepository
    {
        Task<List<ChapterDTO>> GetByIdAsync(Guid seriesId);
        Task CreateAsync(Chapter chapter);

        Task<PageDTO> GetPageByPageNumber(int pageNumber, Guid chapterId);
    }
}
