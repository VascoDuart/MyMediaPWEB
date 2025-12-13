using MyMedia.Data.Models;
using System.Threading.Tasks;

namespace MyMedia.API.Services {
    public interface ITokenService {
        Task<string> CreateToken(ApplicationUser user);
    }
}