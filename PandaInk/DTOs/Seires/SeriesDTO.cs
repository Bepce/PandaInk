using PandaInk.API.DTOs.Chapter;
using PandaInk.API.DTOs.Review;
using System.ComponentModel.DataAnnotations;

namespace PandaInk.API.DTOs.Seires
{
    public class SeriesDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Genre { get; set; }
        [Required]
        public string? CoverImage { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        public string? ReleaseDate { get; set; }

        public string Reviews { get; set; }

        public List<ChapterDTO>? Chapters { get; set; }
    }
}
