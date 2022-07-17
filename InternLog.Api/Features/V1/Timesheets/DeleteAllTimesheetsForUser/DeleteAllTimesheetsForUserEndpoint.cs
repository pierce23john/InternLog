using InternLog.Api.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternLog.Api.Features.V1.Timesheets.DeleteAllTimesheetsForUser
{
    public class DeleteAllTimesheetsForUserEndpoint : EndpointWithoutRequest
    {
        private readonly ITimesheetService _timesheetService;

        public DeleteAllTimesheetsForUserEndpoint(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        public override void Configure()
        {
            Delete(ApiV1Routes.Timesheets.DeleteAllForUser);
            Version(1);
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var userId = Route<Guid>("userId");

            var deleted = await _timesheetService.DeleteAllTimesheetsForUserAsync(userId);

            if (!deleted)
            {
                await SendNotFoundAsync(ct);
            }
            await SendErrorsAsync(cancellation: ct);
        }


    }
}
