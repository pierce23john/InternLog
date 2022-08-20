using InternLog.Domain.Entities;

namespace InternLog.Api.Services.Contracts
{
    public interface ITimesheetService
    {
        Task<List<Timesheet>> GetAllAsync();
        Task<Timesheet> GetByIdAsync(Guid id);
        Task<List<Timesheet>> GetAllByUserIdAsync(Guid userId);
        Task<Timesheet> CreateAsync(Timesheet timesheet);
        Task<bool> UpdateAsync(Timesheet timesheetToUpdate);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAllForUserAsync(Guid userId);
    }
}
