using InternLog.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace InternLog.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();
        public ApplicationUser() { }
        public ApplicationUser(string username) : base(username) { }
    }
}
