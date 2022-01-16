using System.ComponentModel;

namespace InternLog.Api.Contracts.V1.Requests.Timesheets
{
    public class CreateTimesheetRequest
    {
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly TimeIn { get; set; }
        public TimeOnly TimeOut { get; set; }
        [DefaultValue(false)]
        public bool IsAbsent { get; set; }
        [DefaultValue(false)]
        public bool IsHoliday { get; set; }
    }
}
