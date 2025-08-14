using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PandaInk.API.Controllers;
using PandaInk.API.Models;
using PandaInk.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using PandaInk.API.DTOs.Seires;

namespace PandaInk.API.Tests
{
    public class LibraryControllerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<ISeriesRepository> _mockSeriesRepo;
        private readonly Mock<ILibraryRepository> _mockLibraryRepo;
        private readonly LibraryController _controller;
        private readonly ApplicationUser _testUser;

        public LibraryControllerTests()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);

            _mockSeriesRepo = new Mock<ISeriesRepository>();
            _mockLibraryRepo = new Mock<ILibraryRepository>();

            _controller = new LibraryController(_mockUserManager.Object, _mockSeriesRepo.Object, _mockLibraryRepo.Object);

            // Setup test user and claims
            _testUser = new ApplicationUser { Id = "user1", UserName = "testuser" };
            var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "testuser")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userClaims }
            };

            _mockUserManager.Setup(x => x.FindByNameAsync("testuser")).ReturnsAsync(_testUser);
        }

        #region GetLibrary Tests

        [Fact]
        public async Task GetLibrary_ShouldReturnOk_WithUserLibrary()
        {
            // Arrange
            var testUser = new ApplicationUser { Id = "user1", UserName = "testuser" };
            var libraryItems = new List<SeriesDTO>
            {
                new SeriesDTO
                    {
                        Id = Guid.NewGuid(),
                        Title = "Test Series",
                        Description = "Test Description"
                    }
            };

            // Mock UserManager to return testUser
            _mockUserManager.Setup(x => x.FindByNameAsync("testuser"))
                .ReturnsAsync(testUser);

            // Mock library repository to return List<SeriesDTO>
            _mockLibraryRepo.Setup(x => x.GetUserLibraryAsync(testUser))
                .ReturnsAsync(libraryItems);

            // Set up controller User
            var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
        new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "testuser")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userClaims }
            };

            // Act
            var result = await _controller.GetLibrary();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedLibrary = Assert.IsType<List<SeriesDTO>>(okResult.Value);

            Assert.Equal(libraryItems.Count, returnedLibrary.Count);
            Assert.Equal(libraryItems[0].Id, returnedLibrary[0].Id);
            Assert.Equal(libraryItems[0].Title, returnedLibrary[0].Title);
            Assert.Equal(libraryItems[0].Description, returnedLibrary[0].Description);
        }



        [Fact]
        public async Task GetLibrary_ShouldReturnUnauthorized_WhenUserNotFound()
        {
            // Arrange
            _mockUserManager.Setup(x => x.FindByNameAsync("testuser")).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.GetLibrary();

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("User not found.", unauthorizedResult.Value);
        }

        #endregion

        #region AddToLibrary Tests

        [Fact]
        public async Task AddToLibrary_ShouldReturnOk_WhenSeriesAdded()
        {
            // Arrange
            var seriesId = Guid.NewGuid();
            _mockSeriesRepo.Setup(x => x.GetSeriesByIdAsync(seriesId)).ReturnsAsync(new SeriesDTO());
            _mockLibraryRepo.Setup(x => x.LibraryEntryExistsAsync(It.IsAny<Library>())).ReturnsAsync(false);

            // Act
            var result = await _controller.AddToLibrary(seriesId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Series added to library successfully.", okResult.Value);
            _mockLibraryRepo.Verify(x => x.AddToLibraryAsync(It.IsAny<Library>()), Times.Once);
        }

        [Fact]
        public async Task AddToLibrary_ShouldReturnBadRequest_WhenSeriesNotFound()
        {
            // Arrange
            var seriesId = Guid.NewGuid();
            _mockSeriesRepo.Setup(x => x.GetSeriesByIdAsync(seriesId)).ReturnsAsync((SeriesDTO)null);

            // Act
            var result = await _controller.AddToLibrary(seriesId);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Series not found.", badRequest.Value);
        }

        [Fact]
        public async Task AddToLibrary_ShouldReturnBadRequest_WhenSeriesAlreadyInLibrary()
        {
            // Arrange
            var seriesId = Guid.NewGuid();
            _mockSeriesRepo.Setup(x => x.GetSeriesByIdAsync(seriesId)).ReturnsAsync(new SeriesDTO());
            _mockLibraryRepo.Setup(x => x.LibraryEntryExistsAsync(It.IsAny<Library>())).ReturnsAsync(true);

            // Act
            var result = await _controller.AddToLibrary(seriesId);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Series already exists in library.", badRequest.Value);
        }

        #endregion

        #region RemoveFromLibrary Tests

        [Fact]
        public async Task RemoveFromLibrary_ShouldReturnOk_WhenSeriesRemoved()
        {
            // Arrange
            var seriesId = Guid.NewGuid();
            _mockLibraryRepo.Setup(x => x.LibraryEntryExistsAsync(It.IsAny<Library>())).ReturnsAsync(true);

            // Act
            var result = await _controller.RemoveFromLibrary(seriesId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Series removed from library successfully.", okResult.Value);
            _mockLibraryRepo.Verify(x => x.RemoveFromLibrary(It.IsAny<Library>()), Times.Once);
        }

        [Fact]
        public async Task RemoveFromLibrary_ShouldReturnBadRequest_WhenSeriesNotInLibrary()
        {
            // Arrange
            var seriesId = Guid.NewGuid();
            _mockLibraryRepo.Setup(x => x.LibraryEntryExistsAsync(It.IsAny<Library>())).ReturnsAsync(false);

            // Act
            var result = await _controller.RemoveFromLibrary(seriesId);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Series not found in library.", badRequest.Value);
        }

        #endregion

        #region IsInLibrary Tests

        [Fact]
        public async Task IsInLibrary_ShouldReturnTrue_WhenSeriesExists()
        {
            // Arrange
            var seriesId = Guid.NewGuid();
            _mockLibraryRepo.Setup(x => x.LibraryEntryExistsAsync(It.IsAny<Library>())).ReturnsAsync(true);

            // Act
            var result = await _controller.IsInLibrary(seriesId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task IsInLibrary_ShouldReturnFalse_WhenSeriesDoesNotExist()
        {
            // Arrange
            var seriesId = Guid.NewGuid();
            _mockLibraryRepo.Setup(x => x.LibraryEntryExistsAsync(It.IsAny<Library>())).ReturnsAsync(false);

            // Act
            var result = await _controller.IsInLibrary(seriesId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.False((bool)okResult.Value);
        }

        #endregion
    }
}
