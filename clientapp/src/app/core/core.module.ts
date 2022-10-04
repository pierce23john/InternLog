import { NgModule, Optional, SkipSelf } from "@angular/core";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";

import { AuthGuard } from "./guard/auth.guard";
import { NoAuthGuard } from "./guard/no-auth.guard";
import { throwIfAlreadyLoaded } from "./guard/module-import.guard";

import { AuthService } from "./service/auth.service";
import ApiV1Routes from "./constants/apiV1Routes";
import { RouterModule } from "@angular/router";
import { environment } from "@env";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { WithCredentialsInterceptor } from "./interceptors/with-credentials.interceptor";
import { CookieModule } from "ngx-cookie";

@NgModule({
  imports: [HttpClientModule, CommonModule],
  providers: [
    RouterModule,
    AuthGuard,
    NoAuthGuard,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: WithCredentialsInterceptor,
      multi: true,
    },
  ],
  exports: [HttpClientModule, CommonModule],
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, "CoreModule");
  }
}
