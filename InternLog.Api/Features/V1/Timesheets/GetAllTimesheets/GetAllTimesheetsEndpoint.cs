using FastEndpoints;
using InternLog.Api.Extensions;
using InternLog.Api.Services.Contracts;
using InternLog.Domain.Entities;
using Mapster;

namespace InternLog.Api.Features.V1.Timesheets.GetAllTimesheets
{
    public class GetAllTimesheetsEndpoint : EndpointWithMapping<EmptyRequest, object, Timesheet>
    {
        private readonly ITimesheetService _timesheetService;

        public GetAllTimesheetsEndpoint(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        public override void Configure()
        {
            Get(ApiV1Routes.Timesheets.GetAll);
            Version(1);
        }

        public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
        {
            var timesheets = await _timesheetService.GetAllByUserIdAsync(HttpContext.GetUserId());
            await SendOkAsync(timesheets.Select(MapFromEntity), ct);
        }

        public override object MapFromEntity(Timesheet e)
        {
            return e.Adapt<GetTimesheetResponse>();
        }
    }
}
