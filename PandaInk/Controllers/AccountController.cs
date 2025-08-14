using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PandaInk.API.DTOs.Account;
using PandaInk.API.Interfaces;
using PandaInk.API.Models;

namespace PandaInk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        
        public AccountController(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = new ApplicationUser
                {
                    UserName = registerDTO.Username,
                    Email = registerDTO.Email
                };

                var createdUser = await _userManager.CreateAsync(user, registerDTO.Password);

                if(createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDTO
                            {
                                UserName = user.UserName,
                                Email = user.Email,
                                Role =  _userManager.GetRolesAsync(user).ToString(),
                                Token = _tokenService.CreateToken(user, roles)
                            }
                        );
                    }
                    else
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError("RoleError", error.Description);
                        }
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    foreach (var error in createdUser.Errors)
                    {
                        ModelState.AddModelError("UserError", error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var user = await _userManager.FindByNameAsync(loginDTO.Username);
                if (user == null) return Unauthorized("Invalid username or password.");
                var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                if (!result) return Unauthorized("Invalid username or password.");
                var roles = await _userManager.GetRolesAsync(user);

                return Ok(
                    new NewUserDTO
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Role =  _userManager.GetRolesAsync(user).ToString(),
                        Token = _tokenService.CreateToken(user, roles)
                    }
                );
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
