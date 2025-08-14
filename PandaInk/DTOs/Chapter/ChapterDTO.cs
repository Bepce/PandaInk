using PandaInk.API.Models;

namespace PandaInk.API.DTOs.Chapter
{
    public class ChapterDTO
    {
        public Guid Id { get; set; }
        public Guid SeriesId { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<PandaInk.API.Models.Page?> Content { get; set; }
    }
}
