namespace InternLog.Api.Features.V1.Timesheets;

public class GetTimesheetResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public DateOnly Date { get; set; }
    public DateTime TimeIn { get; set; }
    public DateTime TimeOut { get; set; }
    public Guid UserId { get; set; }
    public int HoursRendered => (TimeOut - TimeIn).Hours;
}
