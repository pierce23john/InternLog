using FastEndpoints;

namespace InternLog.Api.Features.V1.Identity.Register;

public class RegisterUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterUserRequestValidator : Validator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {

    }
}

public class RegisterUserSuccessResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}

public class RegisterUserFailedResponse
{
    public IEnumerable<string> Errors { get; set; }
}
