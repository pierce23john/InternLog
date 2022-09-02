using FastEndpoints;

namespace InternLog.Api.Features.V1.Identity.RefreshToken;

public class RefreshTokenRequest
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}

public class RefreshTokenRequestValidator : Validator<RefreshTokenRequest>
{
}

public class RefreshTokenSuccessResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}

public class RefreshTokenFailedResponse
{
    public IEnumerable<string> Errors { get; set; }
}
