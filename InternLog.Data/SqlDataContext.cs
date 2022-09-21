using InternLog.Data.Extensions;
using InternLog.Domain.Entities;
using InternLog.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace InternLog.Data
{
	public class SqlDataContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
	{
		public SqlDataContext(DbContextOptions<SqlDataContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.RegisterEntitiesFromAssembly(typeof(IEntity).Assembly);
			builder.ApplyConfigurationsFromAssembly(typeof(SqlDataContext).Assembly);
		}


		protected override void ConfigureConventions(ModelConfigurationBuilder builder)
		{
			base.ConfigureConventions(builder);

		}
	}

	/// <summary>
	/// Converts <see cref="DateOnly" /> to <see cref="DateTime"/> and vice versa.
	/// </summary>
}