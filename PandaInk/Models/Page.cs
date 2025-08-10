using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaInk.API.Models
{
    public class Page
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public int PageNumber { get; set; }
        [ForeignKey("Chapter")]
        public Guid ChapterId { get; set; }
        public Chapter? Chapter { get; set; }
    }
}
