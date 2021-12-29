using InternLog.Api.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace InternLog.Api.Domain.Entities
{
    public class Timesheet : EntityBase
    {
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
