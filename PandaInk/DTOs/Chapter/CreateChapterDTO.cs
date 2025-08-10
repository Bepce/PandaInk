namespace PandaInk.API.DTOs.Chapter
{
    public class CreateChapterDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int ChapterNumber { get; set; }
        public Guid SeriesId { get; set; }
    }
}
