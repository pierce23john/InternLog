import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { LoginCallbackComponent } from "./page/login-callback/login-callback.component";

import { LoginComponent } from "./page/login/login.component";
import { ProfileComponent } from "./page/profile/profile.component";
import { RegisterComponent } from "./page/register/register.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "login-callback",
        component: LoginCallbackComponent,
      },
      {
        path: "login",
        component: LoginComponent,
      },
      {
        path: "register",
        component: RegisterComponent,
      },
      {
        path: "profile",
        component: ProfileComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class IdentityRoutingModule {}
