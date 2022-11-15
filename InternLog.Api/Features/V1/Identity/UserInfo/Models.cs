namespace InternLog.Api.Features.V1.Identity.UserInfo
{
	public class UserInfoResponse
	{
		public Guid Id { get; set; }
		public string Email { get; set; }
		public string GivenName { get; set; }
		public string Surname { get; set; }
	}
}