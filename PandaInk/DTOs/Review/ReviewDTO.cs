using Microsoft.AspNetCore.Mvc;
using PandaInk.API.Enums;

namespace PandaInk.API.DTOs.Review
{
    public class ReviewDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;

        public RatingEnum Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid SeriesId { get; set; }
    }
}
