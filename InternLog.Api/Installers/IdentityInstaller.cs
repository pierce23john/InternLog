using InternLog.Api.Services.Concretes;
using InternLog.Api.Services.Contracts;
using InternLog.Domain.Entities;
using InternLog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

namespace InternLog.Api.Installers
{
	public class IdentityInstaller : IServiceInstaller
	{
		public Task InstallAsync(IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IIdentityService, IdentityService>();

			services.AddAuthentication((AuthenticationOptions o) =>
			{
				o.DefaultScheme = IdentityConstants.ApplicationScheme;
				o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
			})
			.AddIdentityCookies(identityCookies =>
			{
				identityCookies.ApplicationCookie.Configure(cookieOptions =>
				{
					cookieOptions.SlidingExpiration = true;
					cookieOptions.Cookie.HttpOnly = true;
					cookieOptions.Cookie.SecurePolicy = CookieSecurePolicy.Always;
					cookieOptions.Cookie.SameSite = SameSiteMode.Lax;
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
	}
}