using InternLog.Data.Extensions;
using InternLog.Domain.Entities;
using InternLog.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

			builder.Properties<DateOnly>()
				   .HaveConversion<DateOnlyConverter>()
				   .HaveColumnType("date");

			builder.Properties<TimeOnly>()
				   .HaveConversion<TimeOnlyConverter>()
				   .HaveColumnType("time");
		}
	}

	/// <summary>
	/// Converts <see cref="DateOnly" /> to <see cref="DateTime"/> and vice versa.
	/// </summary>
	public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
	{
		/// <summary>
		/// Creates a new instance of this converter.
		/// </summary>
		public DateOnlyConverter() : base(
				d => d.ToDateTime(TimeOnly.MinValue),
				d => DateOnly.FromDateTime(d))
		{ }
	}

	public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
	{
		public TimeOnlyConverter() : base(
			d => d.ToTimeSpan(),
			d => TimeOnly.FromTimeSpan(d))
		{ }
	}
}