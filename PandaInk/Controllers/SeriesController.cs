using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaInk.API.Data;
using PandaInk.API.DTOs.Review;
using PandaInk.API.Mappers;
using PandaInk.API.Models;

namespace PandaInk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly PandaInkContext _context;

        public SeriesController(PandaInkContext context)
        {
            _context = context;
        }

        // GET: api/series
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Series>>> GetSeries()
        {
            var series = await _context.Series
                .Include(s => s.Reviews)
                .Select(s => s.ToSeriesDTO())
                .ToListAsync();

            return Ok(series);
        }

        // GET: api/series/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Series>> GetSeries(Guid id)
        {
            var series = await _context.Series
                .Include(s => s.Reviews)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (series == null)
            {
                return NotFound();
            }

             return Ok(series.ToSeriesDTO());
        }
    }
}
