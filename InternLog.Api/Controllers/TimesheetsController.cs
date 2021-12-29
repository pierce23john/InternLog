using InternLog.Api.Contracts.V1;
using InternLog.Api.Contracts.V1.Requests.Timesheets;
using InternLog.Api.Contracts.V1.Responses.Timesheets;
using InternLog.Api.Controllers.Base;
using InternLog.Api.Domain.Entities;
using InternLog.Api.Services.Contracts;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace InternLog.Api.Controllers
{
    public class TimesheetsController : ApiControllerBase
    {
        private readonly ITimesheetService _timesheetService;

        public TimesheetsController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpGet(ApiRoutes.Timesheets.GetAll)]
        public async Task<ActionResult<IEnumerable<GetTimesheetRequest>>> GetAllAsync()
        {
            var timesheets = await _timesheetService.GetTimesheetsAsync();
            return Ok(timesheets.Adapt<IEnumerable<CreateTimesheetResponse>>());
        }

        [HttpGet(ApiRoutes.Timesheets.GetById, Name = "GetTimesheetById")]
        public async Task<ActionResult<GetTimesheetRequest>> GetByIdAsync([FromRoute] Guid id)
        {
            var timesheet = await _timesheetService.GetTimesheetByIdAsync(id);
            if (timesheet is null)
            {
                return NotFound();
            }
            return Ok(timesheet);
        }

        //[HttpPost(ApiRoutes.Timesheets.Create)]
        //public async Task<IActionResult> CreateAsync([FromBody] CreateTimesheetRequest createTimesheetRequest)
        //{
        //    var timesheet = createTimesheetRequest.Adapt<Timesheet>();
        //    await _timesheetService.CreateTimesheetAsync(timesheet);

        //    return CreatedAtRoute("GetTimesheetById", new { id = timesheet.Id }, timesheet.Adapt<CreateTimesheetResponse>());
        //}

        [HttpPut(ApiRoutes.Timesheets.FullUpdate)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateTimesheetRequest updateTimesheetRequest)
        {
            var timesheet = updateTimesheetRequest.Adapt<Timesheet>();

            var updated = await _timesheetService.UpdateTimesheetAsync(timesheet);

            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete(ApiRoutes.Timesheets.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id) 
        {
            var deleted = await _timesheetService.DeleteTimesheetAsync(id);

            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
