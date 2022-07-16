import { Component } from "@angular/core";
import { AuthService } from "@app/core/service/auth.service";

@Component({
  selector: "app-nav",
  templateUrl: "navbar.component.html",
  styleUrls: ["navbar.component.scss"],
})
export class NavbarComponent {
  constructor(public authService: AuthService) {}

  logout(): void {
    this.authService.logout();
  }
}
