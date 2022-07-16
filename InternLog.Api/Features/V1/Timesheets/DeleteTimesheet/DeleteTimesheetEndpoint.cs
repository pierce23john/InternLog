using InternLog.Api.Services.Contracts;

namespace InternLog.Api.Features.V1.Timesheets.DeleteTimesheet
{
    public class DeleteTimesheetEndpoint : EndpointWithoutRequest
    {
        private readonly ITimesheetService _timesheetService;

        public DeleteTimesheetEndpoint(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        public override void Configure()
        {
            Delete(ApiV1Routes.Timesheets.Delete);
            Version(1);
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var deleted = await _timesheetService.DeleteTimesheetAsync(id);

            if (!deleted)
            {
                await SendNotFoundAsync();
            }
            await SendNoContentAsync();
        }
    }
}
