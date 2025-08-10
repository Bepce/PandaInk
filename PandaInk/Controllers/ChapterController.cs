using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandaInk.API.DTOs.Chapter;
using PandaInk.API.Interfaces;
using PandaInk.API.Models;

namespace PandaInk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            if (chapter == null)
            {
                return NotFound();
            }
            return Ok(chapter);
        }
        // POST: api/chapter
        [HttpPost]
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
                Content = chapterDTO.Content,
                ChapterNumbr = chapterDTO.ChapterNumber
            };

            await _chapterRepository.CreateAsync(chapter);
            
            return CreatedAtAction(nameof(GetChaptersBySeriesId), new { seriesId = chapter.SeriesId }, chapter);
        }
    }
}
