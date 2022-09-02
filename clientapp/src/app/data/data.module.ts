import { ComponentFactoryResolver, NgModule } from "@angular/core";
import { UserService } from "./service/user.service";
import { TimesheetService } from "./service/timesheet.service";
import { CoreModule } from "@app/core/core.module";
import { AuthService } from "@app/core/service/auth.service";
import { HTTP_INTERCEPTORS } from "@angular/common/http";



@NgModule({
  declarations: [],
  imports: [
    CoreModule
  ],
  providers: [
    UserService,
    TimesheetService,
  ],
})
export class DataModule {}
