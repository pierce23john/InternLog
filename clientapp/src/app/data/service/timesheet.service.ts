import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import ApiV1Routes from "@app/core/constants/apiV1Routes";
import { Observable } from "rxjs";
import { CreateTimesheetRequest } from "../models/createTimesheetRequest";
import { GetTimesheetResponse } from "../models/getTimesheetResponse";

import { Timesheet } from "../schema/timesheet";

@Injectable({
  providedIn: "root",
})
export class TimesheetService {
  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<Array<GetTimesheetResponse>> {
    return this.httpClient.get<GetTimesheetResponse[]>(
      ApiV1Routes.Timesheets.GetAll
    );
  }

  getSingle(id: number): Observable<GetTimesheetResponse> {
    return this.httpClient.get<GetTimesheetResponse>(
      `${ApiV1Routes.Timesheets.GetById.replace("{id}", id.toString())}`
    );
  }

  create(timesheet: CreateTimesheetRequest) {
    return this.httpClient.post(`${ApiV1Routes.Timesheets.Create}`, {
      ...timesheet,
    });
  }
}
