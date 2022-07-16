using FastEndpoints;
using InternLog.Api.Features.V1.Identity.Login;
using InternLog.Api.Services.Contracts;
using System.Net;

namespace InternLog.Api.Features.V1.Identity.Register;

public class RegisterUserEndpoint : Endpoint<RegisterUserRequest, object, RegisterUserMapper>
{
    private readonly IIdentityService _identityService;

    public RegisterUserEndpoint(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public override void Configure()
    {
        Post(ApiV1Routes.Identity.Register);
        AllowAnonymous();
        Description(builder =>
        {
            builder.Accepts<RegisterUserRequest>("application/json");
            builder.Produces<RegisterUserSuccessResponse>();
            builder.Produces<RegisterUserFailedResponse>(400);
        });
        Version(1);
    }

    public override async Task HandleAsync(RegisterUserRequest request, CancellationToken c)
    {
        var registerResult = await _identityService.RegisterAsync(request.Email, request.Password);
        await SendAsync(Map.FromEntity(registerResult), registerResult.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest, c);
    }


}
