using InternLog.Api.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace InternLog.Api.Domain.Entities
{
    public class Timesheet : EntityBase, IUserOwnedEntity
    {
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly TimeIn { get; set; }
        public TimeOnly TimeOut { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
