using InternLog.Api.Data;
using InternLog.Api.Domain.Entities;
using InternLog.Api.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace InternLog.Api.Services.Concretes
{
    public class TimesheetService : ITimesheetService, IUserOwnedEntityService<ApplicationUser, Timesheet>
    {
        private readonly SqlDataContext _dataContext;
        public TimesheetService(SqlDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Timesheet> CreateTimesheetAsync(Timesheet timesheet)
        {
            _dataContext.Set<Timesheet>().Add(timesheet);
            await _dataContext.SaveChangesAsync();
            return timesheet;
        }

        public async Task<bool> DeleteTimesheetAsync(Guid id)
        {
            var timesheet = await GetTimesheetByIdAsync(id);

            if (timesheet is null)
            {
                return false;
            }

            _dataContext.Set<Timesheet>().Remove(timesheet);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
        public async Task<Timesheet> GetTimesheetByIdAsync(Guid id)
        {
            return await _dataContext.Set<Timesheet>().FindAsync(id);
        }

        public async Task<List<Timesheet>> GetTimesheetsAsync()
        {
            return await _dataContext.Set<Timesheet>().AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateTimesheetAsync(Timesheet timesheetToUpdate) 
        {
            _dataContext.Set<Timesheet>().Update(timesheetToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> UserOwnsEntityAsync(Guid id, Guid userId)
        {
            var timesheet = _dataContext.Set<Timesheet>().AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (timesheet == null) 
            {
                return false;
            }

            return timesheet.UserId == userId;
        }
    }
}
