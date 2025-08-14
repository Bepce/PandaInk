using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PandaInk.API.Controllers;
using PandaInk.API.DTOs.Chapter;
using PandaInk.API.DTOs.Seires;
using PandaInk.API.Helpers;
using PandaInk.API.Interfaces;
using PandaInk.API.Models;
using Xunit;

namespace PandaInk.API.Tests
{
    public class SeriesControllerTests
    {
        private readonly Mock<ISeriesRepository> _mockSeriesRepo;
        private readonly SeriesController _controller;

        public SeriesControllerTests()
        {
            _mockSeriesRepo = new Mock<ISeriesRepository>();
            _controller = new SeriesController(null, _mockSeriesRepo.Object);
        }

        private void SetAuthenticatedUser(string username = "testuser")
        {
            var userClaims = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[] { new Claim(ClaimTypes.Name, username) },
                    "mock"
                )
            );

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userClaims }
            };
        }

        [Fact]
        public async Task GetSeries_ReturnsOk_WithListOfSeriesCardDTO()
        {
            // Arrange
            var query = new QueryObject();
            var seriesList = new List<SeriesCardDTO>
            {
                new SeriesCardDTO
                {
                    Id = Guid.NewGuid(),
                    Title = "Series 1",
                    Author = "Author 1",
                    Genre = "Genre 1"
                }
            };

            _mockSeriesRepo
                .Setup(x => x.GetAllSeriesAsync(query))
                .ReturnsAsync(seriesList);

            // Act
            var result = await _controller.GetSeries(query);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedSeries = Assert.IsType<List<SeriesCardDTO>>(okResult.Value);
            Assert.Single(returnedSeries);
            Assert.Equal(seriesList[0].Title, returnedSeries[0].Title);
        }

        [Fact]
        public async Task GetSeriesById_ReturnsOk_WhenSeriesExists()
        {
            // Arrange
            SetAuthenticatedUser();
            var id = Guid.NewGuid();
            var seriesDto = new SeriesDTO
            {
                Id = id,
                Title = "Series Title",
                Author = "Author Name",
                Description = "Description",
                ReleaseDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Genre = "Genre",
                Chapters = new List<ChapterDTO>()
            };

            _mockSeriesRepo
                .Setup(x => x.GetSeriesByIdAsync(id))
                .ReturnsAsync(seriesDto);

            // Act
            var result = await _controller.GetSeries(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedSeries = Assert.IsType<SeriesDTO>(okResult.Value);
            Assert.Equal(id, returnedSeries.Id);
        }

        [Fact]
        public async Task GetSeriesById_ReturnsNotFound_WhenSeriesMissing()
        {
            // Arrange
            SetAuthenticatedUser();
            var id = Guid.NewGuid();

            _mockSeriesRepo
                .Setup(x => x.GetSeriesByIdAsync(id))
                .ReturnsAsync((SeriesDTO)null);

            // Act
            var result = await _controller.GetSeries(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateSeries_ReturnsBadRequest_WhenModelStateInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Title", "Required");

            var series = new SeriesDTO
            {
                Id = Guid.NewGuid(),
                Author = "Author",
                Genre = "Genre"
            };

            // Act
            var result = await _controller.CreateSeries(series);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateSeries_ReturnsBadRequest_WhenSeriesIsNull()
        {
            // Act
            var result = await _controller.CreateSeries(null);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Series data is required.", badRequest.Value);
        }

        [Fact]
        public async Task CreateSeries_ReturnsBadRequest_WhenSeriesAlreadyExists()
        {
            // Arrange
            var series = new SeriesDTO
            {
                Id = Guid.NewGuid(),
                Title = "Existing Series",
                Author = "Author",
                Genre = "Genre"
            };

            _mockSeriesRepo
                .Setup(x => x.SeriesExsistByName(series.Title))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.CreateSeries(series);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Series already exists", badRequest.Value);
        }

        [Fact]
        public async Task CreateSeries_ReturnsCreatedAtAction_WhenValid()
        {
            // Arrange
            var seriesDto = new SeriesDTO
            {
                Id = Guid.NewGuid(),
                Title = "New Series",
                Author = "Author",
                Genre = "Genre",
                ReleaseDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
            };

            _mockSeriesRepo
                .Setup(x => x.SeriesExsistByName(seriesDto.Title))
                .ReturnsAsync(false);

            _mockSeriesRepo
                .Setup(x => x.AddSeries(seriesDto))
                .ReturnsAsync(new Series
                {
                    Id = seriesDto.Id,
                    Title = seriesDto.Title
                });

            // Act
            var result = await _controller.CreateSeries(seriesDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetSeries), createdResult.ActionName);
            Assert.Equal(seriesDto, createdResult.Value);
        }
    }
}
