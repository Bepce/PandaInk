using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PandaInk.API.Exntensions;
using PandaInk.API.Interfaces;
using PandaInk.API.Models;
using PandaInk.API.Repository;

namespace PandaInk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISeriesRepository _seriesRepository;
        private readonly LibraryRepository _libraryRepository;

        public LibraryController(UserManager<ApplicationUser> userManager, ISeriesRepository seriesRepository, LibraryRepository libraryRepository)
        {
            _userManager = userManager;
            _seriesRepository = seriesRepository;
            _libraryRepository = libraryRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetLibrary()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Unauthorized("User not found.");
            }
            var library = await _libraryRepository.GetUserLibraryAsync(user);        
            return Ok(library);
        }

        
    }
}
