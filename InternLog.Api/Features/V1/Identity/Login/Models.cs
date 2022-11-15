using FastEndpoints;

namespace InternLog.Api.Features.V1.Identity.Login;

public class LoginUserRequest
{
	public string Email { get; set; }
	public string Password { get; set; }
}

public class LoginUserRequestValidator : Validator<LoginUserRequest>
{
	public LoginUserRequestValidator()
	{
	}
}

public class LoginUserSuccessResponse
{
	public bool Success { get; set; }
	public string Token { get; set; }
	public string RefreshToken { get; set; }
}

public class LoginUserFailedResponse
{
	public IEnumerable<string> Errors { get; set; }
}