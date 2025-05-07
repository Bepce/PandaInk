using Microsoft.EntityFrameworkCore;
using PandaInk.API.Data;
using PandaInk.API.DTOs.Review;
using PandaInk.API.Interfaces;
using PandaInk.API.Mappers;
using PandaInk.API.Models;

namespace PandaInk.API.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly PandaInkContext _context;

        public ReviewRepository(PandaInkContext context)
        {
            _context = context;
        }
        public async Task<Review> CreateReviewAsync(Review review)
        {
            await _context.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review?> DeleteReviewAsync(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return review;
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<IEnumerable<ReviewDTO?>> GetReviewsBySeriesIdAsync(Guid seriesId)
        {
            await _context.FindAsync<Series>(seriesId);
           
            return await _context.Reviews
                .Where(r => r.SeriesId == seriesId)
                .Select(r => r.ToReviewDTO())
                .ToListAsync();
        }

        public async Task<Review?> UpdateReviewAsync(UpdateReviewDTO reviewDTO)
        {
            var existingReview = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewDTO.Id);
            if (existingReview == null)
            {
                return null;
            }

            existingReview.Content = reviewDTO.Content;
            existingReview.Rating = reviewDTO.Rating;

            await _context.SaveChangesAsync();

            return existingReview;
        }
    }
}
