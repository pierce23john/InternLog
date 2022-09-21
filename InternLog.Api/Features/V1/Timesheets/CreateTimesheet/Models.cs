using System.ComponentModel;

namespace InternLog.Api.Features.V1.Timesheets.CreateTimesheet
{
	public class CreateTimesheetRequest
	{
		public Guid UserId { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public DateTime TimeIn { get; set; }
		public DateTime TimeOut { get; set; }

		[DefaultValue(false)]
		public bool IsAbsent { get; set; }

		[DefaultValue(false)]
		public bool IsHoliday { get; set; }
	}

	public class CreateTimesheetRequestValidator : Validator<CreateTimesheetRequest>
	{
		public CreateTimesheetRequestValidator()
		{
			RuleFor(timesheet => timesheet.TimeIn)
				.LessThan(timesheet => timesheet.TimeOut)
				.WithMessage("Time in must be earlier than time out.");
		}
	}

	public class CreateTimesheetResponse
	{
		public Guid Id { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public DateTime TimeIn { get; set; }
		public DateTime TimeOut { get; set; }
		public Guid UserId { get; set; }
		public int HoursRendered => (TimeOut - TimeIn).Hours;
	}
}