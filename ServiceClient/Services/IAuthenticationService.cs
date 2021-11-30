using ServiceClient.Models;
using System.Threading.Tasks;

namespace ServiceClient.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> GetTokenAsync();
    }
}
