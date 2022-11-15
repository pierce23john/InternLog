namespace InternLog.Api.Features.V1
{
	public static class ApiV1Routes
	{
		public const string Root = "api";
		public const string Version = "v1";
		public const string Base = Root + "/" + Version;

		public static class Timesheets
		{
			public const string GetAll = Base + "/timesheets";
			public const string GetById = Base + "/timesheets/{id}";
			public const string GetAllByUserId = Base + "/identity/{userId}/timesheets";
			public const string Create = Base + "/timesheets";
			public const string FullUpdate = Base + "/timesheets/{id}";
			public const string Delete = Base + "/timesheets/{id}";
			public const string DeleteAllForUser = Base + "/timesheets/{userId}";
		}

		public static class Identity
		{
			public const string Login = Base + "/identity/login";
			public const string Logout = Base + "/identity/logout";
			public const string Register = Base + "/identity/register";
			public const string UserInfo = Base + "/identity/userinfo";
			public const string ConfirmEmail = Base + "/identity/confirm";
			public const string RefreshToken = Base + "/identity/refresh";
		}

		public static class System
		{
			public const string Check = Base + "/system/check";
		}
	}
}