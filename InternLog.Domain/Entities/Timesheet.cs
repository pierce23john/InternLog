using InternLog.Domain.Entities.Base;

namespace InternLog.Domain.Entities
{
	public class Timesheet : EntityBase, IUserOwnedEntity
	{
		public string Description { get; set; }
		public DateOnly Date { get; set; }
		public TimeOnly TimeIn { get; set; }
		public TimeOnly TimeOut { get; set; }
		public bool IsHoliday { get; set; }
		public bool IsAbsent { get; set; }
		public Guid UserId { get; set; }
		public ApplicationUser User { get; set; }
	}
}