namespace InternLog.Api.Contracts.V1
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Timesheets
        {
            public const string GetAll = Base + "/timesheets";
            public const string GetById = Base + "/timesheets/{id}";
            public const string Create = Base + "/timesheets";
            public const string FullUpdate = Base + "/timesheets";
            public const string Delete = Base + "/timesheets";
        }

        public static class WeatherForecasts 
        {
            public const string GetAll = Base + "/weatherforecasts";
        }
    }
}
