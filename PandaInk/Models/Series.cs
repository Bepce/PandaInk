namespace PandaInk.API.Models
{
    public class Series
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? CoverImage { get; set; }

        public string? Author { get; set; }

        public string? Genre { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
