using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PandaInk.API.Controllers;
using PandaInk.API.DTOs.Account;
using PandaInk.API.Interfaces;
using PandaInk.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PandaInk.API.Tests
{
    public class AccountControllerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);

            _mockTokenService = new Mock<ITokenService>();

            _controller = new AccountController(_mockUserManager.Object, _mockTokenService.Object);
        }

        #region Register Tests

        [Fact]
        public async Task Register_ShouldReturnOk_WhenUserCreatedSuccessfully()
        {
            // Arrange
            var dto = new RegisterDTO { Username = "test", Email = "test@test.com", Password = "Pass123!" };

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password))
                            .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User"))
                            .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                            .ReturnsAsync(new List<string> { "User" });

            _mockTokenService.Setup(x => x.CreateToken(It.IsAny<ApplicationUser>(), It.IsAny<IList<string>>()))
                             .Returns("fake-token");

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var newUser = Assert.IsType<NewUserDTO>(okResult.Value);
            Assert.Equal("test", newUser.UserName);
            Assert.Equal("test@test.com", newUser.Email);
            Assert.Equal("fake-token", newUser.Token);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenUserCreationFails()
        {
            // Arrange
            var dto = new RegisterDTO { Username = "test", Email = "test@test.com", Password = "Pass123!" };
            var errors = new IdentityError[] { new IdentityError { Description = "User error" } };

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password))
                            .ReturnsAsync(IdentityResult.Failed(errors));

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var modelState = Assert.IsType<SerializableError>(badRequest.Value);
            Assert.True(modelState.ContainsKey("UserError"));
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenRoleAssignmentFails()
        {
            // Arrange
            var dto = new RegisterDTO { Username = "test", Email = "test@test.com", Password = "Pass123!" };
            var roleErrors = new IdentityError[] { new IdentityError { Description = "Role error" } };

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password))
                            .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User"))
                            .ReturnsAsync(IdentityResult.Failed(roleErrors));

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var modelState = Assert.IsType<SerializableError>(badRequest.Value);
            Assert.True(modelState.ContainsKey("RoleError"));
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenModelStateInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Username", "Required");
            var dto = new RegisterDTO();

            // Act
            var result = await _controller.Register(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region Login Tests

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenUserNotFound()
        {
            // Arrange
            var dto = new LoginDTO { Username = "test", Password = "Pass123!" };
            _mockUserManager.Setup(x => x.FindByNameAsync(dto.Username)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenPasswordInvalid()
        {
            // Arrange
            var dto = new LoginDTO { Username = "test", Password = "Pass123!" };
            var user = new ApplicationUser { UserName = "test", Email = "test@test.com" };

            _mockUserManager.Setup(x => x.FindByNameAsync(dto.Username)).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.CheckPasswordAsync(user, dto.Password)).ReturnsAsync(false);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenCredentialsValid()
        {
            // Arrange
            var dto = new LoginDTO { Username = "test", Password = "Pass123!" };
            var user = new ApplicationUser { UserName = "test", Email = "test@test.com" };

            _mockUserManager.Setup(x => x.FindByNameAsync(dto.Username)).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.CheckPasswordAsync(user, dto.Password)).ReturnsAsync(true);
            _mockUserManager.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new List<string> { "User" });
            _mockTokenService.Setup(x => x.CreateToken(user, It.IsAny<IList<string>>())).Returns("fake-token");

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var newUser = Assert.IsType<NewUserDTO>(okResult.Value);
            Assert.Equal("test", newUser.UserName);
            Assert.Equal("test@test.com", newUser.Email);
            Assert.Equal("fake-token", newUser.Token);
        }

        [Fact]
        public async Task Login_ShouldReturnBadRequest_WhenModelStateInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Username", "Required");
            var dto = new LoginDTO();

            // Act
            var result = await _controller.Login(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion
    }
}
