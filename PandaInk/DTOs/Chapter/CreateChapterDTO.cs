using PandaInk.API.Models;

namespace PandaInk.API.DTOs.Chapter
{
    public class CreateChapterDTO
    {
        public string Title { get; set; } = string.Empty;
        public List<Page> Content { get; set; } 
        public int ChapterNumber { get; set; }
        public Guid SeriesId { get; set; }
    }
}
