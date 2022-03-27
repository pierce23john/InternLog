using InternLog.Api.Contracts.V1;
using InternLog.Api.Controllers.V1;
using InternLog.Api.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternLog.Api.Services.Concretes
{
    public class LinkGeneratorService : ILinkGeneratorService
    {
        private readonly LinkGenerator _linkGenerator;
        private HttpContext _httpContext;

        public LinkGeneratorService(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _linkGenerator = linkGenerator;
        }

        public async Task<string> GenerateConfirmationEmailLink(Guid userId, string token)
        {
            var path = _linkGenerator.GetUriByName(_httpContext, nameof(ApiV1Routes.Identity.ConfirmEmail), new { UserId = userId, Token = token });

            return await Task.FromResult(path);
        }
    }
}
