using PandaInk.API.Models;

namespace PandaInk.API.DTOs.Chapter
{
    public class CreateChapterDTO
    {
        public string Title { get; set; } = string.Empty;
        public int ChapterNumber { get; set; }
        public List<PandaInk.API.Models.Page> Content { get; set; }
        public Guid SeriesId { get; set; }
    }
}
