using PandaInk.API.DTOs.Page;
using PandaInk.API.Models;

namespace PandaInk.API.DTOs.Chapter
{
    public class CreateChapterDTO
    {
        public string Title { get; set; } = string.Empty;
        public int ChapterNumber { get; set; }
        public List<PageDTO> Content { get; set; }
        public Guid SeriesId { get; set; }
    }
}
