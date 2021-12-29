namespace InternLog.Api.Contracts.V1.Requests
{
    public class UpdateTimesheetRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
