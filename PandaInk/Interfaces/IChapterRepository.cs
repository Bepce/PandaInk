using PandaInk.API.DTOs.Chapter;
using PandaInk.API.Models;

namespace PandaInk.API.Interfaces
{
    public interface IChapterRepository
    {
        Task<ChapterDTO> GetByIdAsync(Guid id);
        Task CreateAsync(Chapter chapter);
    }
}
