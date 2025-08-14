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
        private readonly ISeriesRepository _seriesRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(IReviewRepository reviewRepo, UserManager<ApplicationUser> userManager, ISeriesRepository seriesRepository)
        {
            _seriesRepository = seriesRepository;
            _reviewRepository = reviewRepo;
            _userManager = userManager;
        }

        // GET: api/review/{seriesId}
        [HttpGet("{seriesId}")]
        public async Task<ActionResult<Review>> GetReview([FromRoute] Guid id)
        {
            var review = await _reviewRepository.GetReviewsBySeriesIdAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        [HttpGet("{seriesId}/all")]
        public async Task<ActionResult<List<ReviewDTO>>> GetAllReviews(Guid seriesId)
        {
            var reviews = await _reviewRepository.GetReviewsBySeriesIdAsync(seriesId);

            if (reviews == null || !reviews.Any())
            {
                return NotFound("No reviews found for this series.");
            }

            return Ok(reviews);
        }


        [HttpGet("{seriesId}/exists")]
        [Authorize]
        public async Task<bool> CheckUserReview(Guid seriesId)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            return await _reviewRepository.SeriesReviewExistsByUser(user.Id, seriesId);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDTO reviewDTO)
        {
            var reviewModel = reviewDTO.ToReviewFromCreateReviewDTO();

            var series = await _seriesRepository.GetSeriesByIdAsync(reviewDTO.SeriesId);

            if (series == null)
            {
                return BadRequest();
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if (await _reviewRepository.SeriesReviewExistsByUser(user.Id, reviewDTO.SeriesId))
            {
                return BadRequest("You already wrote a review!");
            }

            reviewModel.UserId = user.Id;

            await _reviewRepository.CreateReviewAsync(reviewModel);

            return Ok();
        }

        [HttpPut("{reviewId}")]
        [Authorize]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewDTO reviewDTO)
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

        [HttpDelete]
        [Route("{reviewId}")]
        public async Task<IActionResult> DeleteReview()
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
