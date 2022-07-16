using FastEndpoints;
using InternLog.Domain.Models;

namespace InternLog.Api.Features.V1.Identity.Register
{
    public class RegisterUserMapper : Mapper<RegisterUserRequest, object, AuthenticationResult>
    {
        public RegisterUserMapper()
        {
            
        }

        public override object FromEntity(AuthenticationResult e)
        {
            if (!e.Success)
            {
                return new RegisterUserFailedResponse() 
                {
                    Errors = e.Errors
                };
            }
            else
            {
                return new RegisterUserSuccessResponse()
                {
                    Token = e.Token,
                    RefreshToken = e.RefreshToken
                };
            }
        }
    }
}
