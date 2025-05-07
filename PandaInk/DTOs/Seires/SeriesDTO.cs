using PandaInk.API.DTOs.Review;

namespace PandaInk.API.DTOs.Seires
{
    public class SeriesDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? CoverImage { get; set; }

        public string? Author { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public List<ReviewDTO> Reviews { get; set; }
    }
}
