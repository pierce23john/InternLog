using InternLog.Domain.Models;
using System.Security.Claims;

namespace InternLog.Api.Services.Contracts
{
	public interface IIdentityService
	{
		Task<AuthenticationResult> RegisterAsync(string username, string password);

		Task<AuthenticationResult> ConfirmEmailAsync(Guid userId, string token);

		Task<AuthenticationResult> LoginAsync(string email, string password);

		Task<bool> LogoutAsync();
	}
}