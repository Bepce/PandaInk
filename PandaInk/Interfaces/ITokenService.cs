using PandaInk.API.Models;
using System.Data;

namespace PandaInk.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user, IList<string> roles);
    }
}
