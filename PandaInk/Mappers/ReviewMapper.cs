using PandaInk.API.DTOs.Review;
using PandaInk.API.Models;

namespace PandaInk.API.Mappers
{
    public static class ReviewMapper
    {
        public static Review ToReviewFromCreateReviewDTO(this CreateReviewDTO createReviewDTO)
        {
            return new Review
            {
                Content = createReviewDTO.Content,
                Rating = createReviewDTO.Rating,
                CreatedAt = createReviewDTO.CreatedAt,
                SeriesId = createReviewDTO.SeriesId
            };
        }

        public static ReviewDTO ToReviewDTO(this Review review)
        {
            return new ReviewDTO
            {
                Id = review.Id,
                Content = review.Content,
                Rating = review.Rating,
                CreatedAt = review.CreatedAt,
                SeriesId = review.SeriesId
            };
        }
    }
}
