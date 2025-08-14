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
                ReleaseDate = series.ReleaseDate,
                Reviews = series.Reviews.Select(r => r.ToReviewDTO()).ToList(),
                Chapters = series.Chapters.Select(c => c.ToChapterDTO()).ToList(),
            };
        }

        public static SeriesCardDTO ToSeriesCardDTO(this Series series)
        {
            return new SeriesCardDTO
            {
                Id = series.Id,
                Title = series.Title,
                Genre = series.Genre,
                CoverImage = series.CoverImage,
                Author = series.Author,
                Score = series.Reviews.Any() ? Math.Round(series.Reviews.Average(r => (decimal) r.Rating), 2).ToString() : "No rating yet."
            };
        }
    }
}
