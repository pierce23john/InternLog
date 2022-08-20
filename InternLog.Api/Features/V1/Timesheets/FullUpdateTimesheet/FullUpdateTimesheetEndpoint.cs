using InternLog.Api.Services.Contracts;
using InternLog.Domain.Entities;
using Mapster;

namespace InternLog.Api.Features.V1.Timesheets.FullUpdateTimesheet
{
    public class FullUpdateTimesheetEndpoint : EndpointWithMapping<UpdateTimesheetRequest, GetTimesheetResponse, Timesheet>
    {
        private readonly ITimesheetService _timesheetService;

        public FullUpdateTimesheetEndpoint(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        public override void Configure()
        {
            Put(ApiV1Routes.Timesheets.FullUpdate);
            Version(1);
        }

        public override async Task HandleAsync(UpdateTimesheetRequest request, CancellationToken ct)
        {
            var timesheet = MapToEntity(request);

            var updated = await _timesheetService.UpdateAsync(timesheet);

            if (!updated)
            {
                await SendNotFoundAsync(ct);
            }

            await SendNoContentAsync(ct);
        }

        public override GetTimesheetResponse MapFromEntity(Timesheet e)
        {
            return e.Adapt<GetTimesheetResponse>();
        }

        public override Timesheet MapToEntity(UpdateTimesheetRequest r)
        {
            return r.Adapt<Timesheet>();
        }
    }
}
