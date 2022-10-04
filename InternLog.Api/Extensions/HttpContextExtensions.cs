using System.Security.Claims;

namespace InternLog.Api.Extensions;

public static class HttpContextExtensions
{
	public static Guid GetUserId(this HttpContext context)
	{
		var id = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
		return id != null ? Guid.Parse(id.Value) : Guid.Empty;
	}
}