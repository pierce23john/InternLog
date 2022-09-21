using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using InternLog.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternLog.STS.Services.Concretes
{
	public class CustomProfileService : DefaultProfileService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public CustomProfileService(ILogger<DefaultProfileService> logger, UserManager<ApplicationUser> userManager) : base(logger)
		{
			_userManager = userManager;
		}

		public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			await base.GetProfileDataAsync(context);

			//>Processing
			ApplicationUser user = await _userManager.GetUserAsync(context.Subject);

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.GivenName, user.GivenName),
				new Claim(JwtRegisteredClaimNames.FamilyName, user.Surname),
			};

			context.IssuedClaims = claims;
		}

		public override Task IsActiveAsync(IsActiveContext context)
		{
			return base.IsActiveAsync(context);
		}
	}
}