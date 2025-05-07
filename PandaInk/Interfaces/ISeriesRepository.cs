using PandaInk.API.DTOs.Seires;
using PandaInk.API.Helpers;

namespace PandaInk.API.Interfaces
{
    public interface ISeriesRepository
    {
        Task<IEnumerable<SeriesDTO>> GetAllSeriesAsync(QueryObject query);

        Task<SeriesDTO?> GetSeriesByIdAsync(Guid id);
    }
}
