using System.ComponentModel.DataAnnotations;

namespace PandaInk.API.Models
{
    public class Series
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? CoverImage { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        public string? Genre { get; set; }
        [Required]
        public DateTime? ReleaseDate { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
