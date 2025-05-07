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
        public string? CoverImage { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        public DateTime? ReleaseDate { get; set; }

        public List<ReviewDTO>? Reviews { get; set; }
    }
}
