using PandaInk.API.Models;

namespace PandaInk.API.DTOs.Chapter
{
    public class ChapterDTO
    {
        public string Title { get; set; } = string.Empty;
        public List<PandaInk.API.Models.Page?> Content { get; set; }
    }
}
