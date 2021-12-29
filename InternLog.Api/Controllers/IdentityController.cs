using InternLog.Api.Contracts.V1;
using InternLog.Api.Contracts.V1.Requests.Identity;
using InternLog.Api.Contracts.V1.Responses.Identity;
using InternLog.Api.Controllers.Base;
using InternLog.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternLog.Api.Controllers
{
    public class IdentityController : ApiControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterUserRequest registerUserRequest)
        {
            var registerResult = await _identityService.RegisterAsync(registerUserRequest.Email, registerUserRequest.Password);

            if (!registerResult.Success)
            {
                return BadRequest(new RegisterFailedResponse()
                {
                    Errors = registerResult.Errors
                });
            }

            return Ok(new RegisterSuccessResponse()
            {
                Token = registerResult.Token
            });
        }
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> LoginAsync(LoginUserRequest loginUserRequest)
        {
            var loginResult = await _identityService.LoginAsync(loginUserRequest.Email, loginUserRequest.Password);

            if (!loginResult.Success)
            {
                return Unauthorized(new LoginFailedResponse
                {
                    Errors = loginResult.Errors
                });
            }

            return Ok(new LoginSuccessResponse
            {
                Token = loginResult.Token
            });
        }
    }
}
