using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PandaInk.API.Exntensions;
using PandaInk.API.Interfaces;
using PandaInk.API.Models;
using PandaInk.API.Repository;
using System.Security.Claims;

namespace PandaInk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISeriesRepository _seriesRepository;
        private readonly ILibraryRepository _libraryRepository;

        public LibraryController(UserManager<ApplicationUser> userManager, ISeriesRepository seriesRepository, ILibraryRepository libraryRepository)
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

        [HttpPost("{seriesId}")]
        [Authorize]
        public async Task<IActionResult> AddToLibrary(Guid seriesId)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var series = await _seriesRepository.GetSeriesByIdAsync(seriesId);
            if (series == null) return BadRequest("Series not found.");

            var libraryEntry = new Library
            {
                UserId = user.Id,
                SeriesId = seriesId
            };
            if (await _libraryRepository.LibraryEntryExistsAsync(libraryEntry))
            {
                return BadRequest("Series already exists in library.");
            }

            await _libraryRepository.AddToLibraryAsync(libraryEntry);
            return Ok("Series added to library successfully.");
        }

        [HttpDelete("{seriesId}")]
        [Authorize]
        public async Task<IActionResult> RemoveFromLibrary(Guid seriesId)
        { 
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var libraryEntry = new Library
            {
                UserId = user.Id,
                SeriesId = seriesId
            };
            if (!await _libraryRepository.LibraryEntryExistsAsync(libraryEntry))
            {
                return BadRequest("Series not found in library.");
            }
            await _libraryRepository.RemoveFromLibrary(libraryEntry);
            return Ok("Series removed from library successfully.");
        }

        [HttpGet("{seriesId}")]
        [Authorize]
        public async Task<IActionResult> IsInLibrary (Guid seriesId)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            return Ok(await _libraryRepository.SeriesExistsInUserLibrary(seriesId, user.Id));
        }
    }
}
