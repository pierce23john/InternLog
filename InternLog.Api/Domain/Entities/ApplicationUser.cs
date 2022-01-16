using InternLog.Api.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace InternLog.Api.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity
    {
        public ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();

        public ApplicationUser() { }

        public ApplicationUser(string username) : base(username) { }
    }
}
