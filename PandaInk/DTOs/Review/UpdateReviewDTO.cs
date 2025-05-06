using PandaInk.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace PandaInk.API.DTOs.Review
{
    public class UpdateReviewDTO
    {
        
        public Guid Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public RatingEnum Rating { get; set; }

        public Guid SeriesId { get; set; }
    }
}
