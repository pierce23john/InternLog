import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { OidcSecurityService } from "angular-auth-oidc-client";

@Component({
  selector: "app-root",
  templateUrl: `app.component.html`,
  styleUrls: ["app.component.scss"],
})
export class AppComponent {
  constructor(
    public oidcSecurityService: OidcSecurityService,
    private router: Router
  ) {}

  ngOnInit() {}
}
