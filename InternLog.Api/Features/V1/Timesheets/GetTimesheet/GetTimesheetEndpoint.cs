using InternLog.Api.Services.Contracts;
using InternLog.Domain.Entities;
using Mapster;

namespace InternLog.Api.Features.V1.Timesheets.GetTimesheet
{
    public class GetTimesheetEndpoint : EndpointWithMapping<EmptyRequest, GetTimesheetResponse, Timesheet>
    {
        private readonly ITimesheetService _timesheetService;

        public GetTimesheetEndpoint(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }
        public override void Configure()
        {
            Get(ApiV1Routes.Timesheets.GetById);
            Description(builder =>
            {
                builder.WithName("GetTimesheetById");
            });
        }

        public override GetTimesheetResponse MapFromEntity(Timesheet e)
        {
            return e.Adapt<GetTimesheetResponse>();
        }

        public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var timesheet = await _timesheetService.GetByIdAsync(id);

            if (timesheet is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(MapFromEntity(timesheet), ct);
        }

    }
}
