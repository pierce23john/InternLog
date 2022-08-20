using InternLog.Api.Data;
using InternLog.Api.Services.Contracts;
using InternLog.Domain.Entities;
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

        public async Task<Timesheet> CreateAsync(Timesheet timesheet)
        {
            _dataContext.Set<Timesheet>().Add(timesheet);
            await _dataContext.SaveChangesAsync();
            return timesheet;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var timesheet = await GetByIdAsync(id);

            if (timesheet is null)
            {
                return false;
            }

            _dataContext.Set<Timesheet>().Remove(timesheet);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> DeleteAllForUserAsync(Guid userId)
        {
            var timesheetsForUser = await _dataContext.Set<Timesheet>()
                .Where(timesheet => timesheet.UserId == userId).ToListAsync();

            _dataContext.Set<Timesheet>().RemoveRange(timesheetsForUser);

            var deletedRows = await _dataContext.SaveChangesAsync();

            return deletedRows > 0;
        }

        public async Task<Timesheet> GetByIdAsync(Guid id)
        {
            return await _dataContext.Set<Timesheet>().FindAsync(id);
        }

        public async Task<List<Timesheet>> GetAllAsync()
        {
            return await _dataContext.Set<Timesheet>().AsNoTracking().ToListAsync();
        }

        public async Task<List<Timesheet>> GetAllByUserIdAsync(Guid userId)
        {
            return await _dataContext.Set<Timesheet>().AsNoTracking()
                .Where(timesheet => timesheet.UserId == userId).ToListAsync();
        }


        public async Task<bool> UpdateAsync(Timesheet timesheetToUpdate)
        {
            _dataContext.Set<Timesheet>().Update(timesheetToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public Task<bool> UserOwnsEntityAsync(Guid id, Guid userId)
        {
            var timesheet = _dataContext.Set<Timesheet>().AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (timesheet == null)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(timesheet.UserId == userId);
        }
    }
}