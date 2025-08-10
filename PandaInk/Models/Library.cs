using System.ComponentModel.DataAnnotations.Schema;

namespace PandaInk.API.Models
{
    [Table("Libraries")]
    public class Library
    {
        public string UserId { get; set; } 
        
        public ApplicationUser User { get; set; }

        public Guid SeriesId { get; set; }

        public Series Series { get; set; }

    }
}
