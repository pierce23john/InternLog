using InternLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternLog.Data.EntityTypeConfigurations
{
    public class TimesheetConfiguration : IEntityTypeConfiguration<Timesheet>
    {
        public void Configure(EntityTypeBuilder<Timesheet> builder)
        {
            builder.Property(prop => prop.Description).HasMaxLength(255);

            builder.HasOne(timesheet => timesheet.User)
                   .WithMany(user => user.Timesheets)
                   .HasForeignKey(timesheet => timesheet.UserId);

		}
    }
}
