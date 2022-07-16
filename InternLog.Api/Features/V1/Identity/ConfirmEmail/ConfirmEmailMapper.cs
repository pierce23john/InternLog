using FastEndpoints;
using InternLog.Domain.Models;

namespace InternLog.Api.Features.V1.Identity.ConfirmEmail
{
    public class ConfirmEmailMapper : Mapper<ConfirmEmailRequest, object, AuthenticationResult>
    {
        public ConfirmEmailMapper()
        {

        }

        public override object FromEntity(AuthenticationResult e)
        {
            if (!e.Success)
            {
                return new ConfirmEmailFailedResponse()
                {
                    Errors = e.Errors
                };
            }
            else
            {
                return new ConfirmEmailSuccessResponse()
                {
                    Token = e.Token,
                    RefreshToken = e.RefreshToken
                };
            }
        }
    }
}
