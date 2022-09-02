using FastEndpoints;
using InternLog.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using InternLog.Domain.Models;

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
			foreach (var cookie in HttpContext.Request.Cookies)
			{
				HttpContext.Response.Cookies.Delete(cookie.Key);
			}
			await SendOkAsync();
		}
	}
}