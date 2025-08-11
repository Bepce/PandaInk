using PandaInk.API.DTOs.Seires;
using PandaInk.API.Models;

namespace PandaInk.API.Interfaces
{
    public interface ILibraryRepository
    {
        Task<List<SeriesDTO>> GetUserLibraryAsync(ApplicationUser user);
        Task AddToLibraryAsync(Library libraryEntry);
        Task<bool> LibraryEntryExistsAsync(Library libraryEntry);
        void RemoveFromLibrary(Library libraryEntry);
    }
}
