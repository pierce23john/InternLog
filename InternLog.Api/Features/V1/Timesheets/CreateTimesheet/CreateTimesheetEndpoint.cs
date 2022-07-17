using FluentValidation.Results;
using InternLog.Api.Extensions;
using InternLog.Api.Features.V1.Timesheets.GetTimesheet;
using InternLog.Api.Services.Contracts;
using InternLog.Domain.Entities;
using Mapster;

namespace InternLog.Api.Features.V1.Timesheets.CreateTimesheet
{
    public class CreateTimesheetEndpoint : EndpointWithMapping<CreateTimesheetRequest, CreateTimesheetResponse, Timesheet>
    {
        private readonly ITimesheetService _timesheetService;
        public CreateTimesheetEndpoint(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        public override void Configure()
        {
            Post(ApiV1Routes.Timesheets.Create);
            Version(1);
        }

        public override async Task HandleAsync(CreateTimesheetRequest request, CancellationToken ct)
        {
            request.UserId = HttpContext.GetUserId();
            var timesheet = request.Adapt<Timesheet>();
            await _timesheetService.CreateTimesheetAsync(timesheet);

            await SendCreatedAtAsync<GetTimesheetEndpoint>(new { id = timesheet.Id }, MapFromEntity(timesheet), cancellation: ct);
        }

        public override CreateTimesheetResponse MapFromEntity(Timesheet e)
        {
            return e.Adapt<CreateTimesheetResponse>();
        }
    }
}
