using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PandaInk.API.DTOs.Chapter;
using PandaInk.API.Exntensions;
using PandaInk.API.Interfaces;
using PandaInk.API.Models;

namespace PandaInk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChapterController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IChapterRepository _chapterRepository;
        private readonly ILibraryRepository _libraryRepository;

        public ChapterController(IChapterRepository chapterRepository, UserManager<ApplicationUser> userManager, ILibraryRepository libraryRepository)
        {
            _chapterRepository = chapterRepository;
            _userManager = userManager;
            _libraryRepository = libraryRepository;
        }


        [HttpGet("{chapterId}")]
        [Authorize]
        public async Task<IActionResult> GetChapterById(Guid chapterId)
        {
            var username = User.GetUsername();

            var user = await _userManager.FindByNameAsync(username);


            var chapter = await _chapterRepository.GetByIdAsync(chapterId);
            if (chapter == null)
            {
                return NotFound();
            }

           var libraryEntry = await _libraryRepository.LibraryEntryExistsAsync(new Library
           {
               SeriesId = chapter.SeriesId,
               UserId = user.Id
           });

            if (!libraryEntry)
            {
                return Unauthorized("You do not have access to this chapter.");
            }

            return Ok(chapter);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateChapter([FromBody] CreateChapterDTO chapterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chapter = new Chapter()
            {
                SeriesId = chapterDTO.SeriesId,
                Title = chapterDTO.Title,
                ChapterNumber = chapterDTO.ChapterNumber
            };

            await _chapterRepository.CreateAsync(chapter);
            
            return CreatedAtAction(nameof(GetChapterById), new { chapterId = chapter.Id }, chapter);
        }
    }
}
