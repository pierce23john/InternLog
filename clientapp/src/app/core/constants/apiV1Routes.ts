import { environment } from "@env";

export default class ApiV1Routes {
  static ApiUrl = environment.baseBackendUrl;
  static Root = "api";
  static Version = "v1";
  static Base =
    ApiV1Routes.ApiUrl + "/" + ApiV1Routes.Root + "/" + ApiV1Routes.Version;

  static Identity = class {
    static Base = ApiV1Routes.Base + "/identity";
    static Login = ApiV1Routes.Base + "/identity/login";
    static Register = ApiV1Routes.Base + "/identity/register";
    static ConfirmEmail = ApiV1Routes.Base + "/identity/confirm";
    static Refresh = ApiV1Routes.Base + "/identity/refresh";
    static AllRoutes = Object.values(this).map(value => `${value}`);
  };

  static Timesheets = class {
    static GetAll = ApiV1Routes.Base + "/timesheets";
    static GetAllByUserId = ApiV1Routes.Base + "/identity/{userId}/timesheets";
    static GetById = ApiV1Routes.Base + "/timesheets/{id}";
    static Create = ApiV1Routes.Base + "/timesheets";
  };
}
