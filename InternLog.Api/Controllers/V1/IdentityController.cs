using InternLog.Api.Contracts.V1;
using InternLog.Api.Contracts.V1.Requests.Identity;
using InternLog.Api.Contracts.V1.Responses.Identity;
using InternLog.Api.Controllers.Base;
using InternLog.Api.Domain.Models;
using InternLog.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace InternLog.Api.Controllers.V1
{
    public class IdentityController : ApiControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        [HttpPost(ApiV1Routes.Identity.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterUserRequest registerUserRequest)
        {
            var registerResult = await _identityService.RegisterAsync(registerUserRequest.Email, registerUserRequest.Password);
            return HandleAuthenticationResult(registerResult);
        }
        [HttpPost(ApiV1Routes.Identity.Login)]
        public async Task<IActionResult> LoginAsync(LoginUserRequest loginUserRequest)
        {
            var loginResult = await _identityService.LoginAsync(loginUserRequest.Email, loginUserRequest.Password);
            return HandleAuthenticationResult(loginResult);
        }

        [HttpPost(ApiV1Routes.Identity.RefreshToken)]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            var refreshResult = await _identityService.RefreshTokenAsync(refreshTokenRequest.Token, refreshTokenRequest.RefreshToken);
            return HandleAuthenticationResult(refreshResult);
        }

        [NonAction]
        private IActionResult HandleAuthenticationResult(AuthenticationResult authenticationResult)
        {
            if (!authenticationResult.Success)
            {
                return Unauthorized(new AuthenticationFailedResponse
                {
                    Errors = authenticationResult.Errors
                });
            }

            return Ok(new AuthenticationSuccessResponse
            {
                Token = authenticationResult.Token,
                RefreshToken = authenticationResult.RefreshToken
            });
        }
    }
}
