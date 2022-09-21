using DateOnlyTimeOnly.AspNet.Converters;
using InternLog.Api.Converters;
using InternLog.Api.Features.V1.Timesheets;
using InternLog.Api.Features.V1.Timesheets.CreateTimesheet;
using InternLog.Api.Services.Concretes;
using InternLog.Api.Services.Contracts;
using InternLog.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace InternLog.Api.Installers
{
	public class MvcInstaller : IServiceInstaller
	{
		public Task InstallAsync(IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<JsonOptions>(config => config.UseDateOnlyTimeOnlyStringConverters());

			services.AddFastEndpoints();

			services.AddHttpContextAccessor();
			services.AddScoped<ILinkGeneratorService, LinkGeneratorService>();

			services.AddSwaggerDoc(swaggerSettings =>
			{
				swaggerSettings.DocumentName = "Internlog API";
				swaggerSettings.Title = "InternLog API";
				swaggerSettings.Version = "v1";
			}, serializerSettings =>
			{
				serializerSettings.Converters.Add(new CustomDateOnlyJsonConverter());
				serializerSettings.Converters.Add(new TimeOnlyJsonConverter());
			}, addJWTBearerAuth: true,
			tagIndex: 3,
			maxEndpointVersion: 1,
			shortSchemaNames: true);

			services.AddCors();

			return Task.CompletedTask;
		}
	}
}