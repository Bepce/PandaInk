using PandaInk.API.DTOs.Seires;

namespace PandaInk.API.Interfaces
{
    public interface ISeriesRepository
    {
        Task<IEnumerable<SeriesDTO>> GetAllSeriesAsync();

        Task<SeriesDTO?> GetSeriesByIdAsync(Guid id);
    }
}
