using PandaInk.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace PandaInk.API.DTOs.Review
{
    public class UpdateReviewDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public RatingEnum Rating { get; set; }

        [Required]
        public Guid SeriesId { get; set; }
    }
}
