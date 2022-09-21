import { NgModule, Optional, SkipSelf } from "@angular/core";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";

import { AuthGuard } from "./guard/auth.guard";
import { NoAuthGuard } from "./guard/no-auth.guard";
import { throwIfAlreadyLoaded } from "./guard/module-import.guard";

import { AuthService } from "./service/auth.service";
import {
  JwtConfig,
  JwtInterceptor,
  JwtModule,
  JWT_OPTIONS,
} from "@auth0/angular-jwt";
import ApiV1Routes from "./constants/apiV1Routes";
import { RouterModule } from "@angular/router";
import {
  AuthInterceptor,
  AuthModule,
  LogLevel,
  OidcSecurityService,
} from "angular-auth-oidc-client";
import { environment } from "@env";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

export function jwtOptionsFactory(
  oidcSecurityService: OidcSecurityService
): JwtConfig {
  return {
    tokenGetter: () => {
      let token = "";
      oidcSecurityService.getAccessToken().subscribe((data) => {
        token = data;
      });
      return token;
    },
    allowedDomains: ["localhost:7238"],
    disallowedRoutes: [...ApiV1Routes.Identity.AllRoutes],
  };
}

@NgModule({
  imports: [
    HttpClientModule,
    AuthModule.forRoot({
      config: {
        authority: "https://localhost:5001",
        redirectUrl: `${window.location.origin}/identity/login-callback`,
        postLogoutRedirectUri: `${window.location.origin}/identity/logout-callback`,
        postLoginRoute: "https://localhost:4200/app/home",
        clientId: "internlog.clientapp",
        configId: environment.configId,
        scope: "openid profile offline_access internlog_api",
        responseType: "code",
        silentRenew: true,
        useRefreshToken: true,
        secureRoutes: ["http://localhost:4200/app"],
        logLevel: LogLevel.Debug,
      },
    }),
    JwtModule.forRoot({
      jwtOptionsProvider: {
        provide: JWT_OPTIONS,
        useFactory: jwtOptionsFactory,
        deps: [OidcSecurityService],
      },
    }),
    CommonModule,
  ],
  providers: [
    RouterModule,
    AuthGuard,
    NoAuthGuard,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true,
    },
  ],
  exports: [HttpClientModule, AuthModule, CommonModule],
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, "CoreModule");
  }
}
