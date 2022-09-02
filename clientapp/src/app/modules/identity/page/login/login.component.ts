import { Component } from "@angular/core";
import { AuthService } from "@app/core/service/auth.service";
import { LoginRequest } from "@app/data/models/login";

@Component({
  selector: "login",
  templateUrl: "login.component.html",
})
export class LoginComponent {
  public loginRequest: LoginRequest = {
    username: "ADMIN@ADMIN.com",
    password: "Admin123!",
  };

  constructor(private authService: AuthService) {}

  login() {
    this.authService.login({
      username: this.loginRequest.username,
      password: this.loginRequest.password,
    });
  }
}
