﻿using FastEndpoints;
using InternLog.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using InternLog.Domain.Models;

namespace InternLog.Api.Features.V1.Identity.Login
{
    public class RefreshTokenEndpoint : Endpoint<LoginUserRequest, object, RefreshTokenMapper>
    {
        private readonly IIdentityService _identityService;

        public RefreshTokenEndpoint(IIdentityService identityService)
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
            await SendAsync(Map.FromEntity(loginResult), loginResult.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest, c);
        }


    }
}