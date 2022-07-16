using InternLog.Api.Data;
using InternLog.Api.Options;
using InternLog.Api.Services.Concretes;
using InternLog.Api.Services.Contracts;
using InternLog.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FastEndpoints.Security;

namespace InternLog.Api.Installers
{
    public class IdentityInstaller : IServiceInstaller
    {
        public Task InstallAsync(IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = new JwtOptions();
            configuration.Bind(nameof(jwtOptions), jwtOptions);
            services.AddSingleton(jwtOptions);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthenticationJWTBearer(jwtOptions.Secret);

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
