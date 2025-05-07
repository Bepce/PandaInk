using PandaInk.API.DTOs.Review;
using PandaInk.API.Models;

namespace PandaInk.API.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<ReviewDTO?>> GetReviewsBySeriesIdAsync(Guid seriesId);

        Task<Review> CreateReviewAsync(Review review);

        Task<Review?> UpdateReviewAsync(UpdateReviewDTO reviewDTO);

        Task<Review?> DeleteReviewAsync(Guid id);
    }
}
