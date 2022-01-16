using FluentValidation.AspNetCore;
using InternLog.Api.Contracts.V1.Requests.Timesheets;
using InternLog.Api.Services.Concretes;
using InternLog.Api.Services.Contracts;
using Microsoft.OpenApi.Models;

namespace InternLog.Api.Installers
{
    public class MvcInstaller : IServiceInstaller
    {
        public Task InstallAsync(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(mvcOptions =>
            {

            }).AddFluentValidation(fluentValidationOptions =>
            {
                fluentValidationOptions.DisableDataAnnotationsValidation = true;
                fluentValidationOptions.RegisterValidatorsFromAssembly(typeof(CreateTimesheetRequestValidator).Assembly);
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.UseDateOnlyTimeOnlyStringConverters();
            });


            services.AddEndpointsApiExplorer();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddSwaggerGen(options =>
            {
                options.UseDateOnlyTimeOnlyStringConverters();

                options.SwaggerDoc("v1", new OpenApiInfo() { Title = "InternLog API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "JWT Auth Header",
                    Name = "Authorization"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return Task.CompletedTask;
        }
    }
}
