import { Injectable } from "@angular/core";
import { of, Observable, tap } from "rxjs";
import { HttpClient } from "@angular/common/http";
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
    console.log(cookieService.getAll())
  }

  login(loginRequest: LoginRequest): void {
    this.http
      .post(
        ApiV1Routes.Identity.Login,
        { email: loginRequest.username, password: loginRequest.password },
        {
          withCredentials: true,
        }
      )
      .subscribe((next) => console.log({ next }));
  }

  logout(): Observable<boolean> {
    return of(false);
  }
}
