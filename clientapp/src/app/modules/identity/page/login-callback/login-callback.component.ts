import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { OidcSecurityService } from "angular-auth-oidc-client";

@Component({
  selector: "login-callback",
  template: ` <p>login-callback works!</p> `,
  styles: [],
})
export class LoginCallbackComponent implements OnInit {
  constructor(
    private oidcSecurityService: OidcSecurityService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.oidcSecurityService
      .checkAuth()
      .subscribe(({ isAuthenticated, userData, accessToken, idToken }) => {
        console.log({ isAuthenticated });
        if (isAuthenticated) {
          this.router.navigate(["app", "home"]);
        } else {
          this.oidcSecurityService.authorize();
        }
      });
  }
}
