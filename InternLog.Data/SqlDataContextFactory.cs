using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;

namespace InternLog.Data
{
	public class SqlDataContextFactory : IDesignTimeDbContextFactory<SqlDataContext>
	{
		public SqlDataContext CreateDbContext(string[] args)
		{
			// IDesignTimeDbContextFactory is used usually when you execute EF Core commands like Add-Migration, Update-Database, and so on
			// So it is usually your local development machine environment
			var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			// Prepare configuration builder
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
				//.AddJsonFile("appsettings.json", optional: false)
				//.AddJsonFile($"appsettings.{envName}.json", optional: false)
				.AddUserSecrets("f887fae4-f8b8-4a98-8dd5-18b58ace9e1c")
				.Build();

			// Create DB context with connection from your AppSettings
			var optionsBuilder = new DbContextOptionsBuilder<SqlDataContext>()
				.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));


			return new SqlDataContext(optionsBuilder.Options);
		}
	}
}