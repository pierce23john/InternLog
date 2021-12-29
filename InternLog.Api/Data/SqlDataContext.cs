using InternLog.Api.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternLog.Api.Data
{
    public class SqlDataContext : IdentityDbContext
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