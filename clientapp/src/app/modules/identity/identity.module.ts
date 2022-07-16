import { NgModule } from "@angular/core";

import { LoginComponent } from "./page/login/login.component";
import { RegisterComponent } from "./page/register/register.component";

import { IdentityRoutingModule } from "./identity.routing";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";


@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [IdentityRoutingModule, CommonModule, FormsModule, HttpClientModule],
})
export class IdentityModule {}
