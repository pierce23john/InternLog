import { ComponentFactoryResolver, NgModule } from "@angular/core";
import { UserService } from "./service/user.service";
import { TimesheetService } from "./service/timesheet.service";


@NgModule({
  declarations: [],
  imports: [
  ],
  providers: [
    UserService,
    TimesheetService,
  ],
})
export class DataModule {}
