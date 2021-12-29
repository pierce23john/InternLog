using InternLog.Api.Domain.Entities;

namespace InternLog.Api.Services.Contracts
{
    public interface ITimesheetService
    {
        Task<List<Timesheet>> GetTimesheetsAsync();
        Task<Timesheet> GetTimesheetByIdAsync(Guid id);
        Task<Timesheet> CreateTimesheetAsync(Timesheet timesheet);
        Task<bool> UpdateTimesheetAsync(Timesheet timesheetToUpdate);
        Task<bool> DeleteTimesheetAsync(Guid id);
    }
}
