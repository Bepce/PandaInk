using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PandaInk.API.Controllers;
using PandaInk.API.DTOs.Review;
using PandaInk.API.DTOs.Seires;
using PandaInk.API.Interfaces;
using PandaInk.API.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

public class ReviewControllerTests
{
    private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
    private readonly Mock<IReviewRepository> _mockReviewRepo;
    private readonly Mock<ISeriesRepository> _mockSeriesRepo;
    private readonly ReviewController _controller;

    public ReviewControllerTests()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        _mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
        _mockReviewRepo = new Mock<IReviewRepository>();
        _mockSeriesRepo = new Mock<ISeriesRepository>();

        _controller = new ReviewController(_mockReviewRepo.Object, _mockUserManager.Object, _mockSeriesRepo.Object);

        // Authenticated user with both Name and GivenName claims
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "testuser"),
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "testuser")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "testuser"),
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "testuser")
        }, "mock"))
            }
        };

    }

    [Fact]
    public async Task GetReview_ReturnsOk_WhenReviewExists()
    {
        var seriesId = Guid.NewGuid();
        var review = new ReviewDTO { Id = Guid.NewGuid(), SeriesId = seriesId, Content = "Great!" };
        _mockReviewRepo.Setup(x => x.GetReviewsBySeriesIdAsync(seriesId))
            .ReturnsAsync(new List<ReviewDTO> { review });

        var result = await _controller.GetReview(seriesId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedReview = Assert.IsType<List<ReviewDTO>>(okResult.Value);
        Assert.Single(returnedReview);
        Assert.Equal(review.Id, returnedReview[0].Id);
    }

    [Fact]
    public async Task GetReview_ReturnsNotFound_WhenNoReview()
    {
        var seriesId = Guid.NewGuid();
        _mockReviewRepo.Setup(x => x.GetReviewsBySeriesIdAsync(seriesId))
            .ReturnsAsync((List<ReviewDTO>)null);

        var result = await _controller.GetReview(seriesId);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetAllReviews_ReturnsOk_WhenReviewsExist()
    {
        var seriesId = Guid.NewGuid();
        var reviews = new List<ReviewDTO>
        {
            new ReviewDTO { Id = Guid.NewGuid(), SeriesId = seriesId, Content = "Good!" }
        };
        _mockReviewRepo.Setup(x => x.GetReviewsBySeriesIdAsync(seriesId))
            .ReturnsAsync(reviews);

        var result = await _controller.GetAllReviews(seriesId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsType<List<ReviewDTO>>(okResult.Value);
        Assert.Single(returned);
    }

    [Fact]
    public async Task GetAllReviews_ReturnsNotFound_WhenNoReviews()
    {
        var seriesId = Guid.NewGuid();
        _mockReviewRepo.Setup(x => x.GetReviewsBySeriesIdAsync(seriesId))
            .ReturnsAsync(new List<ReviewDTO>());

        var result = await _controller.GetAllReviews(seriesId);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("No reviews found for this series.", notFoundResult.Value);
    }

    [Fact]
    public async Task CheckUserReview_ReturnsTrue_WhenReviewExists()
    {
        var user = new ApplicationUser { Id = "user1", UserName = "testuser" };
        _mockUserManager.Setup(x => x.FindByNameAsync("testuser")).ReturnsAsync(user);
        _mockReviewRepo.Setup(x => x.SeriesReviewExistsByUser(user.Id, It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var result = await _controller.CheckUserReview(Guid.NewGuid());

        Assert.True(result);
    }

    [Fact]
    public async Task CreateReview_ReturnsBadRequest_WhenSeriesDoesNotExist()
    {
        var reviewDTO = new CreateReviewDTO { SeriesId = Guid.NewGuid(), Content = "Nice!" };
        _mockSeriesRepo.Setup(x => x.GetSeriesByIdAsync(reviewDTO.SeriesId))
            .ReturnsAsync((SeriesDTO)null); // FIX: Return null to simulate missing series

        var result = await _controller.CreateReview(reviewDTO);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task CreateReview_ReturnsBadRequest_WhenUserAlreadyReviewed()
    {
        var seriesId = Guid.NewGuid();
        var reviewDTO = new CreateReviewDTO { SeriesId = seriesId, Content = "Nice!" };
        var user = new ApplicationUser { Id = "user1", UserName = "testuser" };

        _mockSeriesRepo.Setup(x => x.GetSeriesByIdAsync(seriesId)).ReturnsAsync(new SeriesDTO());
        _mockUserManager.Setup(x => x.FindByNameAsync("testuser")).ReturnsAsync(user);
        _mockReviewRepo.Setup(x => x.SeriesReviewExistsByUser(user.Id, seriesId))
            .ReturnsAsync(true);

        var result = await _controller.CreateReview(reviewDTO);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("You already wrote a review!", badRequest.Value);
    }

    [Fact]
    public async Task CreateReview_ReturnsOk_WhenReviewCreated()
    {
        var seriesId = Guid.NewGuid();
        var reviewDTO = new CreateReviewDTO { SeriesId = seriesId, Content = "Great!" };
        var user = new ApplicationUser { Id = "user1", UserName = "testuser" };

        _mockSeriesRepo.Setup(x => x.GetSeriesByIdAsync(seriesId)).ReturnsAsync(new SeriesDTO());
        _mockUserManager.Setup(x => x.FindByNameAsync("testuser")).ReturnsAsync(user);
        _mockReviewRepo.Setup(x => x.SeriesReviewExistsByUser(user.Id, seriesId)).ReturnsAsync(false);
        _mockReviewRepo.Setup(x => x.CreateReviewAsync(It.IsAny<Review>()))
            .ReturnsAsync((Review r) => r);

        var result = await _controller.CreateReview(reviewDTO);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateReview_ReturnsUnauthorized_WhenNotOwner()
    {
        var reviewDTO = new UpdateReviewDTO { Id = Guid.NewGuid(), Content = "Updated" };
        var existingReview = new Review { Id = reviewDTO.Id, UserId = "otherUser", Content = "Old" };
        var user = new ApplicationUser { Id = "user1", UserName = "testuser" };

        _mockReviewRepo.Setup(x => x.UpdateReviewAsync(reviewDTO)).ReturnsAsync(existingReview);
        _mockUserManager.Setup(x => x.FindByNameAsync("testuser")).ReturnsAsync(user);

        var result = await _controller.UpdateReview(reviewDTO);

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("You can only update your own reviews.", unauthorized.Value);
    }

    [Fact]
    public async Task UpdateReview_ReturnsNotFound_WhenReviewDoesNotExist()
    {
        var reviewDTO = new UpdateReviewDTO { Id = Guid.NewGuid(), Content = "Updated" };
        _mockReviewRepo.Setup(x => x.UpdateReviewAsync(reviewDTO))
            .ReturnsAsync((Review)null);

        var result = await _controller.UpdateReview(reviewDTO);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateReview_ReturnsOk_WhenOwnerUpdates()
    {
        var reviewDTO = new UpdateReviewDTO { Id = Guid.NewGuid(), Content = "Updated" };
        var review = new Review { Id = reviewDTO.Id, UserId = "user1", Content = "Old" };
        var user = new ApplicationUser { Id = "user1", UserName = "testuser" };

        _mockReviewRepo.Setup(x => x.UpdateReviewAsync(reviewDTO)).ReturnsAsync(review);
        _mockUserManager.Setup(x => x.FindByNameAsync("testuser")).ReturnsAsync(user);

        var result = await _controller.UpdateReview(reviewDTO);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDTO = Assert.IsType<Review>(okResult.Value);
        Assert.Equal(review.Id, returnedDTO.Id);
    }

    [Fact]
    public async Task DeleteReview_ReturnsNoContent_WhenReviewDeleted()
    {
        var reviewId = Guid.NewGuid();
        var review = new Review { Id = reviewId };
        _mockReviewRepo.Setup(x => x.DeleteReviewAsync(reviewId)).ReturnsAsync(review);

        var result = await _controller.DeleteReview(reviewId);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteReview_ReturnsNotFound_WhenReviewDoesNotExist()
    {
        var reviewId = Guid.NewGuid();
        _mockReviewRepo.Setup(x => x.DeleteReviewAsync(reviewId))
            .ReturnsAsync((Review)null);

        var result = await _controller.DeleteReview(reviewId);

        Assert.IsType<NotFoundResult>(result);
    }
}
