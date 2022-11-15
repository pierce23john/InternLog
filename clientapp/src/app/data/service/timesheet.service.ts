import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import ApiV1Routes from "@app/core/constants/apiV1Routes";
import {
  merge,
  Observable,
  Subject,
  scan,
  catchError,
  EMPTY,
  concatMap,
  mergeWith,
} from "rxjs";
import { CreateTimesheetRequest } from "../models/timesheet";
import {
  CreateTimesheetResponse,
  GetTimesheetResponse,
} from "../models/timesheet";

@Injectable({
  providedIn: "root",
})
export class TimesheetService {
  constructor(private httpClient: HttpClient) {}

  private timesheedAddedSubject = new Subject<string>();
  timesheetAddedAction$ = this.timesheedAddedSubject.asObservable();

  timesheets$ = this.httpClient.get<GetTimesheetResponse[]>(ApiV1Routes.Timesheets.GetAll)
    
  onTimesheetAdded(timesheetId: string) {
    this.timesheedAddedSubject.next(timesheetId);
  }

  getSingle(id: string): Observable<GetTimesheetResponse> {
    return this.httpClient.get<GetTimesheetResponse>(
      `${ApiV1Routes.Timesheets.GetById.replace("{id}", id)}`
    );
  }

  getByUserId(userId: string): Observable<Array<GetTimesheetResponse>> {
    return this.httpClient.get<Array<GetTimesheetResponse>>(
      `${ApiV1Routes.Timesheets.GetAllByUserId.replace("{userId}", userId)}`
    );
  }

  create(
    timesheet: CreateTimesheetRequest
  ): Observable<CreateTimesheetResponse> {
    return this.httpClient.post<CreateTimesheetResponse>(
      `${ApiV1Routes.Timesheets.Create}`,
      {
        ...timesheet,
      }
    );
  }
}
