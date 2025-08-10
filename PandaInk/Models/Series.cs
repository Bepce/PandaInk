using System.ComponentModel.DataAnnotations;

namespace PandaInk.API.Models
{
    [Tags("Series")]
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
        public List<Chapter> Chapters { get; set; } = new List<Chapter>();
        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<Library> Libraries { get; set; } = new List<Library>();
    }
}
