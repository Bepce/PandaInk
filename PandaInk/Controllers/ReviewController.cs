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
        public async Task<ActionResult<Review>> GetReview([FromRoute] Guid id)
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

            var seriesId = await _context.Series
                .Where(s => s.Id == reviewDTO.SeriesId)
                .Select(s => s.Id)
                .FirstOrDefaultAsync();


            if(seriesId == Guid.Empty)
            {
                return BadRequest();
            }

            _context.Reviews.Add(reviewModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReview), new { id = seriesId }, reviewDTO);
        }

        // PUT: api/review/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateReview([FromRoute] Guid id, [FromBody] UpdateReviewDTO reviewDTO)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            review.Content = reviewDTO.Content;
            review.Rating = reviewDTO.Rating;

            _context.Entry(review).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(review.ToReviewDTO());
        }

        // DELETE: api/review/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteReview([FromRoute] Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
