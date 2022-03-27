using InternLog.Api.Contracts.V1;
using InternLog.Api.Contracts.V1.Requests.Timesheets;
using InternLog.Api.Contracts.V1.Responses.Timesheets;
using InternLog.Api.Controllers.Base;
using InternLog.Api.Services.Contracts;
using InternLog.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternLog.Api.Controllers.V1
{
    public class TimesheetsV1Controller : ApiControllerBase
    {
        private readonly ITimesheetService _timesheetService;

        public TimesheetsV1Controller(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpGet(ApiV1Routes.Timesheets.GetAll)]
        public async Task<ActionResult<IEnumerable<GetTimesheetRequest>>> GetAllAsync()
        {
            var timesheets = await _timesheetService.GetTimesheetsAsync();
            return Ok(timesheets.Adapt<IEnumerable<CreateTimesheetResponse>>());
        }

        [HttpGet(ApiV1Routes.Timesheets.GetById, Name = "GetTimesheetById")]
        public async Task<ActionResult<GetTimesheetRequest>> GetByIdAsync([FromRoute] Guid id)
        {
            var timesheet = await _timesheetService.GetTimesheetByIdAsync(id);
            if (timesheet is null)
            {
                return NotFound();
            }
            return Ok(timesheet.Adapt<GetTimesheetRequest>());
        }

        [HttpPost(ApiV1Routes.Timesheets.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTimesheetRequest createTimesheetRequest)
        {
            var timesheet = createTimesheetRequest.Adapt<Timesheet>();
            await _timesheetService.CreateTimesheetAsync(timesheet);

            return CreatedAtRoute("GetTimesheetById", new { id = timesheet.Id }, timesheet.Adapt<CreateTimesheetResponse>());
        }

        [HttpPut(ApiV1Routes.Timesheets.FullUpdate)]
        public async Task<IActionResult> UpdateAsync(UpdateTimesheetRequest updateTimesheetRequest)
        {
            var timesheet = updateTimesheetRequest.Adapt<Timesheet>();

            var updated = await _timesheetService.UpdateTimesheetAsync(timesheet);

            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete(ApiV1Routes.Timesheets.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var deleted = await _timesheetService.DeleteTimesheetAsync(id);

            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete(nameof(ApiV1Routes.Timesheets.DeleteAllForUser))]
        [Authorize(Policy = "DeleteTimesheetForUser")]
        public async Task<IActionResult> DeleteAllTimesheetsForUserAsync(Guid userId)
        {
            var deleted = await _timesheetService.DeleteAllTimesheetsForUserAsync(userId);

            if (!deleted)
            {
                return NotFound();
            }
            return BadRequest();
        }
    }
}
