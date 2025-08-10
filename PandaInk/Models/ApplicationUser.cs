using Microsoft.AspNetCore.Identity;

namespace PandaInk.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Library> Libraries { get; set; } = new List<Library>();

    }
}
