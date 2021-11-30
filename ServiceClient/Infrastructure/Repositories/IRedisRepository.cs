using ServiceClient.Models;
using System.Threading.Tasks;

namespace ServiceClient.Infrastructure.Repositories
{
    public interface IRedisRepository
    {
        Task<AuthenticationResponse> GetTokenAsync();
        Task<AuthenticationResponse> RefreshTokenAsync();
    }
}
