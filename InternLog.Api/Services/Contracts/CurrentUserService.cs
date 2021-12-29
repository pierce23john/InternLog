using InternLog.Api.Services.Concretes;

namespace InternLog.Api.Services.Contracts
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string UserId => _httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == "id").Value;
    }
}
