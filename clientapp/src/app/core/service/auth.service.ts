import { Injectable } from "@angular/core";
import { of, Observable, tap } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { LoginResponse } from "@app/data/models/login";
import ApiV1Routes from "../constants/apiV1Routes";
import { Router } from "@angular/router";
import { LoginRequest } from "@app/data/models/login";
import { OidcSecurityService } from "angular-auth-oidc-client";
import { environment } from "@env";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  userData$ = this.oidcSecurityService.getUserData(environment.configId);

  constructor(
    private router: Router,
    private oidcSecurityService: OidcSecurityService
  ) {}

  login(): void {
    this.oidcSecurityService.authorize();
  }

  logout(): Observable<boolean> {
    this.oidcSecurityService.logoff();
    return of(false);
  }

  public get isLoggedIn(): boolean {
    let isLoggedIn = false;
    this.oidcSecurityService.isAuthenticated$.subscribe(
      ({ isAuthenticated }) => {
        isLoggedIn = isAuthenticated;
      }
    );

    return isLoggedIn;
  }
}
