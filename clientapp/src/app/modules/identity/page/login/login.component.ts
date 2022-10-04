import { Component, OnInit } from "@angular/core";
import { AuthService } from "@app/core/service/auth.service";
import { LoginRequest } from "@app/data/models/login";

@Component({
  selector: "login",
  templateUrl: "login.component.html",
})
export class LoginComponent implements OnInit {
  public loginRequest: LoginRequest = {
    username: "ADMIN@ADMIN.com",
    password: "Admin123!",
  };

  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  login() {
    this.authService.login(this.loginRequest);
  }
}
