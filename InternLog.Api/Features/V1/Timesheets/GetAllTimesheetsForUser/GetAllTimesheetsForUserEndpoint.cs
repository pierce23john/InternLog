using FastEndpoints;
using InternLog.Api.Extensions;
using InternLog.Api.Services.Contracts;
using InternLog.Domain.Entities;
using Mapster;

namespace InternLog.Api.Features.V1.Timesheets.GetAllTimesheets
{
    public class GetAllTimesheetsForUserEndpoint : EndpointWithMapping<EmptyRequest, object, Timesheet>
    {
        private readonly ITimesheetService _timesheetService;

        public GetAllTimesheetsForUserEndpoint(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        public override void Configure()
        {
            Get(ApiV1Routes.Timesheets.GetAllByUserId);
            Version(1);
        }

        public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
        {
            if (HttpContext.GetUserId() != Route<Guid>("userId"))
            {
                await SendForbiddenAsync(ct);
            }
            
            var timesheets = await _timesheetService.GetAllByUserIdAsync(HttpContext.GetUserId());
            await SendOkAsync(timesheets.Select(MapFromEntity), ct);
        }

        public override object MapFromEntity(Timesheet e)
        {
            return e.Adapt<GetTimesheetResponse>();
        }
    }
}
