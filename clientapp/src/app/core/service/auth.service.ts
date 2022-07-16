import { Injectable } from "@angular/core";
import { of, Observable, tap } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { LoginResponse } from "@app/data/schema/loginResponse";
import { JwtHelperService } from "@auth0/angular-jwt";
import { LoginRequest } from "@app/data/schema/loginRequest";
import ApiV1Routes from "../constants/apiV1Routes";
import { Router } from "@angular/router";

const defaultUser = {
  username: "ADMIN@ADMIN.com",
  password: "Admin123!",
  token: "",
};

@Injectable({
  providedIn: "root",
})
export class AuthService {

  constructor(private httpClient: HttpClient, private router: Router) {}

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

    const helper = new JwtHelperService();


    return new Date() < helper.getTokenExpirationDate(this.authToken);
  }
}
