using InternLog.Api.Domain.Entities;
using InternLog.Api.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternLog.Api.Data
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
            builder.RegisterEntitiesFromAssembly(typeof(SqlDataContext).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(SqlDataContext).Assembly);
        }
    }
}