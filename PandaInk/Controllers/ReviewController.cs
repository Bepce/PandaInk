using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaInk.API.Data;
using PandaInk.API.DTOs.Review;
using PandaInk.API.Interfaces;
using PandaInk.API.Mappers;
using PandaInk.API.Models;
using System.Runtime.InteropServices;

namespace PandaInk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly PandaInkContext _context;
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(PandaInkContext context, IReviewRepository reviewRepo)
        {
            _context = context;
            _reviewRepository = reviewRepo;
        }

        // GET: api/review/{seriesId}
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview([FromRoute] Guid id)
        {
            var review = await _reviewRepository.GetReviewsBySeriesIdAsync(id);

            if (review == null)
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

            await _reviewRepository.CreateReviewAsync(reviewModel);

            return CreatedAtAction(nameof(GetReview), new { id = seriesId }, reviewDTO);
        }

        // PUT: api/review/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateReview([FromRoute] Guid id, [FromBody] UpdateReviewDTO reviewDTO)
        {
            var review = await _reviewRepository.UpdateReviewAsync(reviewDTO);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(review.ToReviewDTO());
        }

        // DELETE: api/review/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteReview([FromRoute] Guid id)
        {
            var review = await _reviewRepository.DeleteReviewAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
