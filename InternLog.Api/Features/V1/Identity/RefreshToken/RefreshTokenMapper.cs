using FastEndpoints;
using InternLog.Domain.Models;

namespace InternLog.Api.Features.V1.Identity.RefreshToken
{
    public class RefreshTokenMapper : Mapper<RefreshTokenRequest, object, AuthenticationResult>
    {
        public RefreshTokenMapper()
        {

        }

        public override object FromEntity(AuthenticationResult e)
        {
            if (!e.Success)
            {
                return new RefreshTokenFailedResponse()
                {
                    Errors = e.Errors
                };
            }
            else
            {
                return new RefreshTokenSuccessResponse()
                {
                    Token = e.Token,
                    RefreshToken = e.RefreshToken
                };
            }
        }
    }
}
