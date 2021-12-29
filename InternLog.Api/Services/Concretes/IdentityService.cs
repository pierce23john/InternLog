using InternLog.Api.Domain.Entities;
using InternLog.Api.Domain.Models;
using InternLog.Api.Options;
using InternLog.Api.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InternLog.Api.Services.Concretes
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(SignInManager<ApplicationUser> signInManager, JwtOptions jwtOptions)
        {
            _signInManager = signInManager;
            _jwtOptions = jwtOptions;
        }

        public async Task<AuthenticationResult> RegisterAsync(string username, string password)
        {
            var existingUser = await _signInManager.UserManager.FindByEmailAsync(username);

            if (existingUser is not null)
            {
                return new AuthenticationResult
                {
                    Errors = new List<string> { "Email is already in use." }
                };
            }

            ApplicationUser newUser = new(username)
            {
                Email = username
            };

            var registerResult = await _signInManager.UserManager.CreateAsync(newUser, password);

            if (!registerResult.Succeeded)
            {
                return new AuthenticationResult()
                {
                    Errors = registerResult.Errors.Select(error => error.Description)
                };
            }

            return GenerateAuthenticationResult(newUser);
        }

        private AuthenticationResult GenerateAuthenticationResult(ApplicationUser newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("id", newUser.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow.AddHours(2),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationResult()
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var existingUser = await _signInManager.UserManager.FindByEmailAsync(email);

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

            return GenerateAuthenticationResult(existingUser);
        }

        public async Task<AuthenticationResult> ConfirmEmailAsync(string email, string token)
        {
            var existingUser = await _signInManager.UserManager.FindByNameAsync(email);

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

            var confirmationResult = await _signInManager.UserManager.ConfirmEmailAsync(existingUser, token);

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
    }
}
