using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaInk.API.Data;
using PandaInk.API.DTOs.Review;
using PandaInk.API.DTOs.Seires;
using PandaInk.API.Helpers;
using PandaInk.API.Interfaces;
using PandaInk.API.Mappers;
using PandaInk.API.Models;

namespace PandaInk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly PandaInkContext _context;
        private readonly ISeriesRepository _seriesRepository;

        public SeriesController(PandaInkContext context, ISeriesRepository seriesRepo)
        {
            _context = context;
            _seriesRepository = seriesRepo;

        }

        // GET: api/series
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Series>>> GetSeries([FromQuery] QueryObject query)
        {
            var series = await _seriesRepository.GetAllSeriesAsync(query);

            return Ok(series);
        }

        // GET: api/series/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Series>> GetSeries(Guid id)
        {
            var series = await _seriesRepository.GetSeriesByIdAsync(id);

            if (series == null)
            {
                return NotFound();
            }

            series.Chapters.OrderByDescending(c => c.Title);

             return Ok(series);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateSeries([FromBody] SeriesDTO series)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (series == null)
            {
                return BadRequest("Series data is required.");
            }

            var seriesExists = await _seriesRepository.SeriesExsistByName(series.Title);

            if (seriesExists)
            {
                return BadRequest("Series already exists");
            }
            _seriesRepository.AddSeries(series);           
            return CreatedAtAction(nameof(GetSeries), new { id = series.Id }, series);
        }
    }
}
