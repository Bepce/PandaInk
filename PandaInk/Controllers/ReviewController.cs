using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaInk.API.Data;
using PandaInk.API.DTOs.Review;
using PandaInk.API.Exntensions;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(PandaInkContext context, IReviewRepository reviewRepo, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _reviewRepository = reviewRepo;
            _userManager = userManager;
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
        [Authorize]
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

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            reviewModel.UserId = user.Id;

            await _reviewRepository.CreateReviewAsync(reviewModel);

            return CreatedAtAction(nameof(GetReview), new { id = seriesId }, reviewDTO);
        }

        // PUT: api/review/{id}
        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> UpdateReview([FromRoute] Guid id, [FromBody] UpdateReviewDTO reviewDTO)
        {
            var review = await _reviewRepository.UpdateReviewAsync(reviewDTO);

            if (review == null)
            {
                return NotFound();
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if (review.UserId != user.Id)
            {
                return Unauthorized("You can only update your own reviews.");
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
