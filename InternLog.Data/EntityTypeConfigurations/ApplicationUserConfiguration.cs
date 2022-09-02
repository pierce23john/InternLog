using InternLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternLog.Data.EntityTypeConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		builder.Property(user => user.GivenName).HasMaxLength(100);
		builder.Property(user => user.Surname).HasMaxLength(100);
	}
}