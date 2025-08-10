using PandaInk.API.Models;

namespace PandaInk.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
