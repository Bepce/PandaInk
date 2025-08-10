using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaInk.API.Models
{
    public class Chapter
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public int ChapterNumbr { get; set; }
        [Required]
        public string Content { get; set; } = string.Empty;
        [ForeignKey("Series")]
        public Guid SeriesId { get; set; }
        public Series? Series { get; set; }
    }
}
