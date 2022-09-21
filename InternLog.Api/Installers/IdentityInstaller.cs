using InternLog.Api.Options;
using InternLog.Api.Services.Concretes;
using InternLog.Api.Services.Contracts;
using InternLog.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FastEndpoints.Security;
using InternLog.Data;
using IdentityServer4.AccessTokenValidation;
using System.Security.Claims;

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

			services.AddAuthentication(config =>
			{
				config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
						.AddJwtBearer(options =>
						{
							// base-address of your identityserver
							options.Authority = "https://localhost:5001";

							// if you are using API resources, you can specify the name here
							options.Audience = "internlog-api";
								

							// IdentityServer emits a typ header by default, recommended extra check
							options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
						});

			services
				.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
				.AddEntityFrameworkStores<SqlDataContext>();

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