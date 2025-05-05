using PandaInk.API.DTOs.Seires;
using PandaInk.API.Models;

namespace PandaInk.API.Mappers
{
    public static class SeriesMapper
    {
        public static SeriesDTO ToSeriesDTO(this Series series)
        {
            return new SeriesDTO
            {
                Id = series.Id,
                Title = series.Title,
                Description = series.Description,
                CoverImage = series.CoverImage,
                Author = series.Author,
                ReleaseDate = series.ReleaseDate
            };
        }
    }
}
