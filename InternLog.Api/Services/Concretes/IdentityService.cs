using FastEndpoints.Security;

using InternLog.Api.Options;
using InternLog.Api.Services.Contracts;
using InternLog.Data;
using InternLog.Domain.Entities;
using InternLog.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InternLog.Api.Services.Concretes
{
	public class IdentityService : IIdentityService
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly JwtOptions _jwtOptions;
		private readonly SqlDataContext _dataContext;
		private readonly IEmailService _emailService;
		private readonly ILinkGeneratorService _linkGeneratorService;
		private readonly UserManager<ApplicationUser> _userManager;

		public IdentityService(SignInManager<ApplicationUser> signInManager, JwtOptions jwtOptions, SqlDataContext dataContext, IEmailService emailService, ILinkGeneratorService linkGeneratorService)
		{
			_signInManager = signInManager;
			_jwtOptions = jwtOptions;
			_dataContext = dataContext;
			_emailService = emailService;
			_linkGeneratorService = linkGeneratorService;
			_userManager = signInManager.UserManager;
		}

		public async Task<AuthenticationResult> RegisterAsync(string username, string password)
		{
			var existingUser = await _userManager.FindByEmailAsync(username);

			if (existingUser is not null)
			{
				return new AuthenticationResult
				{
					Errors = new List<string> { "Email is already in use." }
				};
			}

			ApplicationUser newUser = new(username)
			{
				Email = username,
				EmailConfirmed = true,
			};

			var registerResult = await _userManager.CreateAsync(newUser, password);

			if (!registerResult.Succeeded)
			{
				return new AuthenticationResult()
				{
					Errors = registerResult.Errors.Select(error => error.Description)
				};
			}

			var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

			var confirmationLink = await _linkGeneratorService.GenerateConfirmationEmailLink(newUser.Id, emailConfirmationToken);

			await _emailService.SendEmailAsync(newUser.Email, "Verify your Email", $"<a href='{confirmationLink}'>Click here to verify</a>");
			return await GenerateAuthenticationResultForUserAsync(newUser);
		}

		public async Task<AuthenticationResult> LoginAsync(string email, string password)
		{
			var existingUser = await _userManager.FindByEmailAsync(email);

			if (existingUser is null)
			{
				return new AuthenticationResult
				{
					Errors = new[]
					{
						"User not found."
					}
				};
			}

			var loginResult = await _signInManager.CheckPasswordSignInAsync(existingUser, password, false);

			if (!loginResult.Succeeded)
			{
				return new AuthenticationResult
				{
					Errors = new[]
					{
						"Invalid Login."
					}
				};
			}

			return await GenerateAuthenticationResultForUserAsync(existingUser);
		}

		public async Task<AuthenticationResult> ConfirmEmailAsync(Guid userId, string token)
		{
			var existingUser = await _userManager.FindByIdAsync(userId.ToString());

			if (existingUser is null)
			{
				return new AuthenticationResult
				{
					Errors = new[]
					{
						"User not found."
					}
				};
			}

			var confirmationResult = await _userManager.ConfirmEmailAsync(existingUser, token);

			if (!confirmationResult.Succeeded)
			{
				return new AuthenticationResult
				{
					Errors = new[]
					{
						"Unable to confirm account."
					}
				};
			}

			return new AuthenticationResult()
			{
				Success = true,
			};
		}

		private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(ApplicationUser user)
		{
			var tokenId = Guid.NewGuid().ToString();

			var jwtToken = JWTBearer.CreateToken(
				signingKey: _jwtOptions.Secret,
				expireAt: DateTime.UtcNow.Add(_jwtOptions.TokenLifetime),
				permissions: null,
				roles: null,
				claims: new (string claimType, string claimValue)[]
				{
					("id", user.Id.ToString()),
					(JwtRegisteredClaimNames.Jti, tokenId),
					(JwtRegisteredClaimNames.Sub, user.Email),
					(JwtRegisteredClaimNames.Email, user.Email),
					(JwtRegisteredClaimNames.GivenName, user.GivenName),
					(JwtRegisteredClaimNames.FamilyName, user.Surname),
				});

			RefreshToken refreshToken = new()
			{
				JwtId = tokenId,
				CreationDate = DateTime.UtcNow,
				UserId = user.Id,
				ExpiryDate = DateTime.UtcNow.AddMonths(6),
			};

			await _dataContext.AddAsync(refreshToken);
			await _dataContext.SaveChangesAsync();

			return new AuthenticationResult()
			{
				Success = true,
				Token = jwtToken,
				RefreshToken = refreshToken.Id.ToString()
			};
		}
	}
}