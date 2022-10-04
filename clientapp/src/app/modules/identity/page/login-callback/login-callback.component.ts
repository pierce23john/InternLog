import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "login-callback",
  template: ` <p>login-callback works!</p> `,
  styles: [],
})
export class LoginCallbackComponent implements OnInit {
  constructor(
    private router: Router
  ) {}

  ngOnInit(): void {

  }
}
