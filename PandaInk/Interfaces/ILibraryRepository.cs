using PandaInk.API.Models;

namespace PandaInk.API.Interfaces
{
    public interface ILibraryRepository
    {
        Task<List<Series>> GetUserLibraryAsync(ApplicationUser user);
    }
}
