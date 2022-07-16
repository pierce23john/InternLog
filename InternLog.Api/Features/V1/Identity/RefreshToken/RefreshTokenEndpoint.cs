using FastEndpoints;
using InternLog.Api.Features.V1.Identity.Login;
using InternLog.Api.Services.Contracts;
using System.Net;

namespace InternLog.Api.Features.V1.Identity.RefreshToken;

public class RefreshTokenEndpoint : Endpoint<RefreshTokenRequest, object, RefreshTokenMapper>
{
    private readonly IIdentityService _identityService;

    public RefreshTokenEndpoint(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public override void Configure()
    {
        Post(ApiV1Routes.Identity.RefreshToken);
        AllowAnonymous();
        Description(builder =>
        {
            builder.Accepts<LoginUserRequest>("application/json");
            builder.Produces<LoginUserSuccessResponse>();
            builder.Produces<LoginUserFailedResponse>(400);
        });
        Version(1);
    }

    public override async Task HandleAsync(RefreshTokenRequest request, CancellationToken c)
    {
        var loginResult = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);
        await SendAsync(Map.FromEntity(loginResult), loginResult.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest, c);
    }


}
