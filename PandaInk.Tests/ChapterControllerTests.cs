using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PandaInk.API.Controllers;
using PandaInk.API.DTOs.Chapter;
using PandaInk.API.DTOs.Page;
using PandaInk.API.Interfaces;
using PandaInk.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace PandaInk.API.Tests
{
    public class ChapterControllerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<IChapterRepository> _mockChapterRepo;
        private readonly Mock<ILibraryRepository> _mockLibraryRepo;
        private readonly ChapterController _controller;

        public ChapterControllerTests()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);

            _mockChapterRepo = new Mock<IChapterRepository>();
            _mockLibraryRepo = new Mock<ILibraryRepository>();

            _controller = new ChapterController(_mockChapterRepo.Object, _mockUserManager.Object, _mockLibraryRepo.Object);


            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "testuser"),
                new Claim(ClaimTypes.Role, "Admin")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        #region GetChapterById Tests

        [Fact]
        public async Task GetChapterById_ShouldReturnNotFound_WhenChapterDoesNotExist()
        {
            // Arrange
            var chapterId = Guid.NewGuid();
            _mockChapterRepo.Setup(x => x.GetByIdAsync(chapterId)).ReturnsAsync((ChapterDTO)null);

            // Act
            var result = await _controller.GetChapterById(chapterId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetChapterById_ShouldReturnUnauthorized_WhenLibraryEntryDoesNotExist()
        {
            // Arrange
            var chapterId = Guid.NewGuid();
            var seriesId = Guid.NewGuid();
            var testUser = new ApplicationUser { Id = "user-id-1", UserName = "testuser" };

            _mockUserManager.Setup(x => x.FindByNameAsync("testuser"))
                .ReturnsAsync(testUser);

            _mockChapterRepo.Setup(x => x.GetByIdAsync(chapterId))
                .ReturnsAsync(new ChapterDTO { Id = chapterId, SeriesId = seriesId });

            _mockLibraryRepo.Setup(x => x.LibraryEntryExistsAsync(It.IsAny<Library>()))
                .ReturnsAsync(false);

            var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "testuser")
            }, "mock"));
            
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userClaims }
            };

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userClaims }
            };

            // Act
            var result = await _controller.GetChapterById(chapterId);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("You do not have access to this chapter.", unauthorizedResult.Value);
        }

        [Fact]
        public async Task GetChapterById_ShouldReturnOk_WhenUserHasAccess()
        {
            // Arrange
            var chapterId = Guid.NewGuid();
            var chapter = new ChapterDTO { Id = chapterId, SeriesId = Guid.NewGuid() };
            var user = new ApplicationUser { UserName = "testuser", Id = "user1" };

            var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "testuser")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userClaims }
            };

            _mockChapterRepo.Setup(x => x.GetByIdAsync(chapterId)).ReturnsAsync(chapter);
            _mockUserManager.Setup(x => x.FindByNameAsync("testuser")).ReturnsAsync(user);
            _mockLibraryRepo.Setup(x => x.LibraryEntryExistsAsync(It.IsAny<Library>())).ReturnsAsync(true);

            // Act
            var result = await _controller.GetChapterById(chapterId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(chapter, okResult.Value);
        }

        #endregion

        #region CreateChapter Tests

        [Fact]
        public async Task CreateChapter_ShouldReturnBadRequest_WhenModelStateInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Title", "Required");
            var dto = new CreateChapterDTO();

            // Act
            var result = await _controller.CreateChapter(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateChapter_ShouldReturnCreated_WhenChapterValid()
        {
            // Arrange
            var dto = new CreateChapterDTO
            {
                SeriesId = Guid.NewGuid(),
                Title = "Chapter 1",
                ChapterNumber = 1,
                Content = new List<PageDTO>
                {
                    new PageDTO { PageNumber = 1, ImageUrl = "url1" },
                    new PageDTO { PageNumber = 2, ImageUrl = "url2" }
                }
            };

            Chapter savedChapter = null;
            _mockChapterRepo.Setup(x => x.CreateAsync(It.IsAny<Chapter>()))
                .Returns<Chapter>(chapter => { savedChapter = chapter; return Task.CompletedTask; });

            // Act
            var result = await _controller.CreateChapter(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedChapter = Assert.IsType<Chapter>(createdResult.Value);

            Assert.Equal(savedChapter, returnedChapter);
            Assert.Equal(dto.Title, returnedChapter.Title);
            Assert.Equal(2, returnedChapter.Content.Count);
        }

        #endregion
    }
}
