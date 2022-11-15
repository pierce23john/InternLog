using FastEndpoints;
using InternLog.Domain.Models;

namespace InternLog.Api.Features.V1.Identity.Login
{
	public class RefreshTokenMapper : Mapper<LoginUserRequest, object, AuthenticationResult>
	{
		public RefreshTokenMapper()
		{
		}

		public override object FromEntity(AuthenticationResult e)
		{
			if (!e.Success)
			{
				return new LoginUserFailedResponse()
				{
					Errors = e.Errors
				};
			}
			else
			{
				return new LoginUserSuccessResponse()
				{
					Success = true
				};
			}
		}
	}
}