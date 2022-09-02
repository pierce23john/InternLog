using FastEndpoints;
using InternLog.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using InternLog.Domain.Models;

namespace InternLog.Api.Features.V1.Identity.Login
{
	public class LoginUserEndpoint : Endpoint<LoginUserRequest, object, RefreshTokenMapper>
	{
		private readonly IIdentityService _identityService;

		public LoginUserEndpoint(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		public override void Configure()
		{
			Post(ApiV1Routes.Identity.Login);
			AllowAnonymous();
			Description(builder =>
			{
				builder.Accepts<LoginUserRequest>("application/json");
				builder.Produces<LoginUserSuccessResponse>();
				builder.Produces<LoginUserFailedResponse>(400);
			});
			Version(1);
		}

		public override async Task HandleAsync(LoginUserRequest request, CancellationToken c)
		{
			AuthenticationResult loginResult = await _identityService.LoginAsync(request.Email, request.Password);
			HttpContext.Response.Cookies.Append("refreshToken", loginResult.RefreshToken, new() { SameSite = SameSiteMode.None, Domain = ".app.localhost", HttpOnly = true, Expires = DateTime.UtcNow.AddMonths(6), Secure = true });

			await SendAsync(Map.FromEntity(loginResult), loginResult.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest, c);
		}
	}
}