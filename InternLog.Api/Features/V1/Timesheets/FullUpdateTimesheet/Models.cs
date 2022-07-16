namespace InternLog.Api.Features.V1.Timesheets.FullUpdateTimesheet
{
    public class UpdateTimesheetRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
