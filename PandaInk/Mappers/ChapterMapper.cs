using PandaInk.API.DTOs.Chapter;
using PandaInk.API.Models;

namespace PandaInk.API.Mappers
{
    public static class ChapterMapper
    {
        public static ChapterDTO ToChapterDTO(this Chapter chapter)
        {
            return new ChapterDTO
            {
                Id = chapter.Id,
                SeriesId = chapter.SeriesId,
                Title = chapter.Title,
                Content = chapter.Content
            };
        }
    }
}
