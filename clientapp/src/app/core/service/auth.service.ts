import { Injectable } from "@angular/core";
import { of, Observable, tap } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { LoginResponse } from "@app/data/models/loginResponse";
import { JwtHelperService } from "@auth0/angular-jwt";
import ApiV1Routes from "../constants/apiV1Routes";
import { Router } from "@angular/router";
import { LoginRequest } from "@app/data/models/loginRequest";

@Injectable({
  providedIn: "root",
})
export class AuthService {

  constructor(private httpClient: HttpClient, private router: Router, private jwtHelper: JwtHelperService) {}

  login(loginContext: LoginRequest): void {
    this.httpClient
      .post<LoginResponse>(ApiV1Routes.Identity.Login, {
        email: loginContext.username,
        password: loginContext.password,
      })
      .subscribe({
        next: (data) => {
          this.authToken = data.token;
          this.router.navigate(["/app/home"]);
        },
        error: (err) => {
          console.log("error", err);
        },
      });
  }

  logout(): Observable<boolean> {
    this.authToken = '';
    this.router.navigate(["/identity/login"]);
    return of(false);
  }

  public set authToken(token: string){
      localStorage.setItem('token', token)
  }

  public get authToken(): string {
    return localStorage.getItem('token');
  }

  public get isLoggedIn(): boolean {
    if (!this.authToken) {
      return false;
    }
    return new Date() < this.jwtHelper.getTokenExpirationDate();
  }
}
