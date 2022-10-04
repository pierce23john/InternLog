using InternLog.Api.Options;
using InternLog.Api.Services.Concretes;
using InternLog.Api.Services.Contracts;
using InternLog.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using InternLog.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace InternLog.Api.Installers
{
	public class IdentityInstaller : IServiceInstaller
	{
		public Task InstallAsync(IServiceCollection services, IConfiguration configuration)
		{
			var jwtOptions = new JwtOptions();
			configuration.Bind(nameof(jwtOptions), jwtOptions);
			services.AddSingleton(jwtOptions);

			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IIdentityService, IdentityService>();

			services.AddAuthentication((AuthenticationOptions o) =>
			{
				o.DefaultScheme = IdentityConstants.ApplicationScheme;
				o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
			}).AddIdentityCookies(identityCookies =>
			{
				identityCookies.ApplicationCookie.Configure(cookieOptions =>
				{
					cookieOptions.SlidingExpiration = true;
					cookieOptions.Cookie.HttpOnly = false;
					cookieOptions.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
					cookieOptions.Cookie.SameSite = SameSiteMode.None;
					cookieOptions.Cookie.Domain = "localhost";
				});
			});

			services
				.AddIdentityCore<ApplicationUser>(options =>
				{
					options.SignIn.RequireConfirmedAccount = false;
					options.Stores.MaxLengthForKeys = 128;
				})
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<SqlDataContext>()
				.AddSignInManager<SignInManager<ApplicationUser>>();

			return Task.CompletedTask;
		}

		private Task OnTokenValidated(TokenValidatedContext arg)
		{
			return Task.CompletedTask;
		}

		private Task OnForbidden(ForbiddenContext arg)
		{
			return Task.CompletedTask;
		}

		private Task OnChallenge(JwtBearerChallengeContext arg)
		{
			return Task.CompletedTask;
		}

		private Task OnAuthenticationFailed(AuthenticationFailedContext arg)
		{
			return Task.CompletedTask;
		}

		private Task OnMessageReceived(MessageReceivedContext messageReceivedContext)
		{
			return Task.CompletedTask;
		}
	}
}