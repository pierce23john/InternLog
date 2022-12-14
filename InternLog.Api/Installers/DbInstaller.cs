using InternLog.Api.Services.Concretes;
using InternLog.Api.Services.Contracts;
using InternLog.Data;
using InternLog.Data.Services.Concretes;
using Microsoft.EntityFrameworkCore;

namespace InternLog.Api.Installers
{
	public class DbInstaller : IServiceInstaller
	{
		public Task InstallAsync(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<SqlDataContext>(options =>
			{
				options.UseSqlServer(connectionString);
			});
			services.AddDatabaseDeveloperPageExceptionFilter();

			services.AddScoped<ITimesheetService, TimesheetService>();
			return Task.CompletedTask;
		}
	}
}