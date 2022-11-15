using InternLog.Api.Services.Contracts;

namespace InternLog.Api.Features.V1.Identity.UserInfo
{
	public class UserInfoEndpoint : Endpoint<EmptyRequest, object>
	{
		private readonly IIdentityService _identityService;

		public UserInfoEndpoint(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		public override void Configure()
		{
			Get(ApiV1Routes.Identity.UserInfo);
			Description(builder =>
			{
				builder.Accepts<EmptyRequest>("application/json");
				builder.Produces<object>();
			});
			Version(1);
		}

		public override async Task HandleAsync(EmptyRequest request, CancellationToken c)
		{
			await SendOkAsync(HttpContext.User.Claims.ToDictionary(keySelector => keySelector.Type, valueSelector => valueSelector.Value));
		}
	}
}