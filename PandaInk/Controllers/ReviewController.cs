using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaInk.API.Data;
using PandaInk.API.DTOs.Review;
using PandaInk.API.Mappers;
using PandaInk.API.Models;

namespace PandaInk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly PandaInkContext _context;

        public ReviewController(PandaInkContext context)
        {
            _context = context;
        }

        // GET: api/review/{seriesId}
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview([FromRoute]Guid id)
        {
            var review = await _context.Reviews
                .Where(r => r.SeriesId == id)
                .Select(r => r.ToReviewDTO())
                .ToListAsync();

            if (review.Count == 0)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // POST: api/review
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDTO reviewDTO)
        {
            var reviewModel = reviewDTO.ToReviewFromCreateReviewDTO();

            _context.Reviews.Add(reviewModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReview), new { id = reviewModel.Id });
        }
    }
}
