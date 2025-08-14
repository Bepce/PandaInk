using PandaInk.API.DTOs.Seires;
using PandaInk.API.Helpers;
using PandaInk.API.Models;

namespace PandaInk.API.Interfaces
{
    public interface ISeriesRepository
    {
        Task<IEnumerable<SeriesCardDTO>> GetAllSeriesAsync(QueryObject query);

        Task<SeriesDTO?> GetSeriesByIdAsync(Guid id);
        Task<Series> AddSeries(SeriesDTO series);
        Task<bool> SeriesExsistByName(string name);
    }
}
