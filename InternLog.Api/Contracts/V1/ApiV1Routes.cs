﻿namespace InternLog.Api.Contracts.V1
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
            public const string Create = Base + "/timesheets";
            public const string FullUpdate = Base + "/timesheets/{id}";
            public const string Delete = Base + "/timesheets{id}";
            public const string DeleteAllForUser = Base + "/timesheets/{userId}";
        }

        public static class WeatherForecasts
        {
            public const string GetAll = Base + "/weatherforecasts";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string ConfirmEmail = Base + "/identity/confirm";
            public const string RefreshToken = Base + "/identity/refresh";
        }
    }
}