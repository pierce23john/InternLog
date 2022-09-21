import { NgModule } from "@angular/core";
import { LoginComponent } from "./page/login/login.component";
import { RegisterComponent } from "./page/register/register.component";
import { IdentityRoutingModule } from "./identity.routing";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { LoginCallbackComponent } from "./page/login-callback/login-callback.component";
import { CoreModule } from "@app/core/core.module";
import { SharedModule } from "@app/shared/shared.module";
import { ProfileComponent } from './page/profile/profile.component';

@NgModule({
  declarations: [LoginComponent, RegisterComponent, LoginCallbackComponent, ProfileComponent],
  imports: [
    SharedModule,
    IdentityRoutingModule,
  ],
})
export class IdentityModule {}
