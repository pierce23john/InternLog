using InternLog.Api.Domain.Models;

namespace InternLog.Api.Services.Contracts
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string username, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    }
}
