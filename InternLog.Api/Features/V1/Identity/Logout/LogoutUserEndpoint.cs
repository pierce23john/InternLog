using InternLog.Api.Services.Contracts;

namespace InternLog.Api.Features.V1.Identity.Login
{
	public class LogoutUserEndpoint : Endpoint<EmptyRequest, EmptyResponse>
	{
		private readonly IIdentityService _identityService;

		public LogoutUserEndpoint(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		public override void Configure()
		{
			Post(ApiV1Routes.Identity.Logout);
			AllowAnonymous();
			Version(1);
		}

		public override async Task HandleAsync(EmptyRequest request, CancellationToken c)
		{
			await _identityService.LogoutAsync();
			await SendOkAsync(c);
		}
	}
}