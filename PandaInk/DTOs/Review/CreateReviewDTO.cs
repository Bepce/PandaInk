using PandaInk.API.Enums;

namespace PandaInk.API.DTOs.Review
{
    public class CreateReviewDTO
    {
        public string Content { get; set; } = string.Empty;

        public RatingEnum Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid SeriesId { get; set; }
    }
}
