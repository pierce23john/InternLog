using Microsoft.OpenApi.Models;

namespace InternLog.Api.Installers
{
    public class MvcInstaller : IServiceInstaller
    {
        public Task InstallAsync(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
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
