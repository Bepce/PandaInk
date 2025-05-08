using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PandaInk.API.DTOs.Account;
using PandaInk.API.Models;

namespace PandaInk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
                    if (roleResult.Succeeded) return Ok("User created successfully.");
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
    }
}
