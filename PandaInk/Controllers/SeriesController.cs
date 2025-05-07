using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaInk.API.Data;
using PandaInk.API.DTOs.Review;
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
        public async Task<ActionResult<Series>> GetSeries(Guid id)
        {
            var series = await _seriesRepository.GetSeriesByIdAsync(id);

            if (series == null)
            {
                return NotFound();
            }

             return Ok(series);
        }
    }
}
