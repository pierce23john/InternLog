import { Injectable } from "@angular/core";
import { of, Observable, tap } from "rxjs";
import { HttpClient, HttpResponse, HttpStatusCode } from "@angular/common/http";
import { LoginResponse } from "@app/data/models/login";
import ApiV1Routes from "../constants/apiV1Routes";
import { Router } from "@angular/router";
import { LoginRequest } from "@app/data/models/login";
import { environment } from "@env";
import { CookieService } from "ngx-cookie";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  constructor(
    private router: Router,
    private cookieService: CookieService,
    private http: HttpClient
  ) {
    console.log(cookieService.getAll());
  }

  userData$ = this.http.get(ApiV1Routes.Identity.UserInfo);

  login(loginRequest: LoginRequest): void {
    let response: LoginResponse;

    this.http
      .post<LoginResponse>(ApiV1Routes.Identity.Login, {
        email: loginRequest.username,
        password: loginRequest.password,
      })
      .subscribe({
        next: (value) => {
          response = value;
        },
        complete: () => {
          this.router.navigate(["app", "home"]);
        },
      });
  }

  logout(): Observable<boolean> {
    let response: Observable<boolean> = of(false);
    this.http
      .post(ApiV1Routes.Identity.Logout, {}, { observe: "response" })
      .subscribe({
        next: (res) => {
          if (res.status == HttpStatusCode.Ok) {
            this.router.navigate(["identity", "login"]);
          }
          response = of(true);
        },
      });
    return response;
  }
}
