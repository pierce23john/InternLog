import { Component, OnInit } from "@angular/core";
import { AuthService } from "@app/core/service/auth.service";

@Component({
  templateUrl: "./identity-layout.component.html",
  styleUrls: ["./identity-layout.component.scss"],
})
export class IdentityLayoutComponent implements OnInit {
  opened: boolean = true;

  constructor(public authService: AuthService) {}

  ngOnInit(): void {}

  login() {}

  logout(): void {}
}
