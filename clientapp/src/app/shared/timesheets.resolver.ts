import { Injectable } from "@angular/core";
import {
  Router,
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from "@angular/router";
import { GetTimesheetResponse } from "@app/data/models/timesheet";
import { TimesheetService } from "@app/data/service/timesheet.service";
import { Observable, of } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class TimesheetsResolver implements Resolve<GetTimesheetResponse[]> {
  constructor(private timesheetService: TimesheetService) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<GetTimesheetResponse[]> {
    return this.timesheetService.timesheets$;
  }
}
