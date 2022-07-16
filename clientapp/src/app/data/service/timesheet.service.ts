import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import ApiV1Routes from "@app/core/constants/apiV1Routes";
import { Observable } from "rxjs";

import { Timesheet } from "../schema/timesheet";

@Injectable({
  providedIn: "root",
})
export class TimesheetService {
  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<Array<Timesheet>> {
    return this.httpClient.get<Timesheet[]>(ApiV1Routes.Timesheets.GetAll);
  }

  getSingle(id: number): Observable<Timesheet> {
    return this.httpClient.get<Timesheet>(
      `${ApiV1Routes.Timesheets.GetById.replace("{id}", id.toString())}`
    );
  }
}
