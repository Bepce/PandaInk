using PandaInk.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaInk.API.Models
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public RatingEnum Rating { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("Series")]
        public Guid SeriesId { get; set; }

        public Series Series { get; set; }
    }
}