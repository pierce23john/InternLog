namespace InternLog.Api.Contracts.V1.Requests.Timesheets
{
    public class UpdateTimesheetRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
