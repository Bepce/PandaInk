namespace PandaInk.API.Models
{
    public class Review
    {
        public Guid Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid SeriesId { get; set; }

        public Series Series { get; set; } = new Series();
    }
}