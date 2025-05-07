using PandaInk.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace PandaInk.API.DTOs.Review
{
    public class CreateReviewDTO
    {
        [Required]
        [Length(1, 500, ErrorMessage = "Content must be between 1 and 500 characters.")]
        public string Content { get; set; } = string.Empty;

        [Required]
        public RatingEnum Rating { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid SeriesId { get; set; }
    }
}
