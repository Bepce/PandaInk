using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandaInk.API.DTOs.Chapter;
using PandaInk.API.Interfaces;
using PandaInk.API.Models;

namespace PandaInk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterRepository _chapterRepository;
        public ChapterController(IChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;
        }
        // GET: api/chapter/{seriesId}
        [HttpGet("{seriesId}")]
        public async Task<IActionResult> GetChaptersBySeriesId(Guid seriesId)
        {
            var chapter = await _chapterRepository.GetByIdAsync(seriesId);
            if (chapter == null || !chapter.Any())
            {
                return NotFound();
            }
            return Ok(chapter);
        }

        // GET: api/chapter/pageNumber}
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetChaptersByPageId(int? pageNumber, Guid chapterId)
        {
            if (pageNumber == null) pageNumber = 1;
            var page = await _chapterRepository.GetPageByPageNumber(pageNumber, chapterId);
            if (page == null )
            {
                return NotFound();
            }
            return Ok(page);
        }

        // POST: api/chapter
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
            
            return CreatedAtAction(nameof(GetChaptersBySeriesId), new { seriesId = chapter.SeriesId }, chapter);
        }
    }
}
