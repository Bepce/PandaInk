using PandaInk.API.Models;

namespace PandaInk.API.Interfaces
{
    public interface ILibraryRepository
    {
        Task<List<Series>> GetUserLibraryAsync(ApplicationUser user);
        Task AddToLibraryAsync(Library libraryEntry);
        Task<bool> LibraryEntryExistsAsync(Library libraryEntry);
        void RemoveFromLibrary(Library libraryEntry);
    }
}
