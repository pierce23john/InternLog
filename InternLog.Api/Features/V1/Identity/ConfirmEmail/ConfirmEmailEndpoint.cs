using FastEndpoints;
using InternLog.Api.Features.V1.Identity.Login;
using InternLog.Api.Services.Contracts;
using System.Net;

namespace InternLog.Api.Features.V1.Identity.ConfirmEmail
{
    public class ConfirmEmailEndpoint : Endpoint<LoginUserRequest, object, ConfirmEmailMapper>
    {
        private readonly IIdentityService _identityService;

        public ConfirmEmailEndpoint(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public override void Configure()
        {
            Post(ApiV1Routes.Identity.ConfirmEmail);
            AllowAnonymous();
            Description(builder =>
            {
                builder.Accepts<ConfirmEmailRequest>("application/json");
                builder.Produces<ConfirmEmailSuccessResponse>();
                builder.Produces<ConfirmEmailFailedResponse>(400);
            });
            Version(1);
        }

        public override async Task HandleAsync(LoginUserRequest request, CancellationToken c)
        {
            var loginResult = await _identityService.LoginAsync(request.Email, request.Password);
            await SendAsync(Map.FromEntity(loginResult), loginResult.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest, c);
        }
    }
}