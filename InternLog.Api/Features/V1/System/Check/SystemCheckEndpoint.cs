namespace InternLog.Api.Features.V1.System.Check
{
    public class SystemCheckEndpoint : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get(ApiV1Routes.System.Check);
            AllowAnonymous();
            Version(1);
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            await SendOkAsync(new
            {
                Message = "Hello from API"
            });
        }
    }
}
