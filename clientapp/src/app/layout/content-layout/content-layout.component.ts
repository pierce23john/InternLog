import { Component, OnInit } from "@angular/core";
import { AuthService } from "@app/core/service/auth.service";

@Component({
  selector: "app-content-layout",
  templateUrl: "./content-layout.component.html",
  styleUrls: ["./content-layout.component.scss"],
})
export class ContentLayoutComponent implements OnInit {
  opened: boolean = true;

  constructor(public authService: AuthService) {}

  ngOnInit(): void {}

  login() {}

  logout(): void {}
}
