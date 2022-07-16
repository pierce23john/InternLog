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

export function jwtOptionsFactory(authService: AuthService): JwtConfig {
  return {
    tokenGetter: () => {
      return authService.authToken;
    },
    allowedDomains: ["localhost:7238"],
    disallowedRoutes: [...ApiV1Routes.Identity.AllRoutes]
  };
}

@NgModule({
  imports: [
    HttpClientModule,
    JwtModule.forRoot({
      jwtOptionsProvider: {
        provide: JWT_OPTIONS,
        useFactory: jwtOptionsFactory,
        deps: [AuthService],
      },
    }),
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
})
export class CoreModule {
constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, "CoreModule");
  }
}
