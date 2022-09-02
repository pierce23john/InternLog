import { Component, OnInit } from "@angular/core";
import { GetTimesheetResponse } from "@app/data/models/timesheet";
import { TimesheetService } from "@app/data/service/timesheet.service";
import { MdbModalRef, MdbModalService } from "mdb-angular-ui-kit/modal";
import { NewTimesheetModalComponent } from "./new-timesheet-modal.component";
import { FormGroup, FormControl } from "@angular/forms";
import {
  catchError,
  combineLatest,
  EMPTY,
  map,
  filter,
  tap,
  startWith,
  forkJoin,
  Subject,
  zip,
} from "rxjs";
import { Timesheet } from "@app/data/schema/timesheet";

@Component({
  selector: "home",
  templateUrl: "home.component.html",
})
export class HomeComponent {
  modalRef: MdbModalRef<NewTimesheetModalComponent> | null = null;

  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  rangeFilters$ = combineLatest([
    this.range.controls.start.valueChanges.pipe(filter((d) => Boolean(d))),
    this.range.controls.end.valueChanges.pipe(filter((d) => Boolean(d))),
  ]).pipe(
    filter(([start, end]) => Boolean(start) && Boolean(end)),
    map(([start, end]) => ({ start, end }))
  );

  timesheetsWithAdd$ = combineLatest([
    this.timesheetService.timesheets$,
    this.rangeFilters$.pipe(startWith(null)),
  ]).pipe(
    tap(([timesheets, range]) => {
      console.log({ timesheets });
      console.log({ range });
    }),
    map(([timesheets, range]) =>
      timesheets.filter(
        (sheet) =>
          (!Boolean(range?.start) && !Boolean(range?.end)) ||
          (new Date(sheet.date) >= range.start &&
            new Date(sheet.date) <= range.end)
      )
    ),
    catchError((error) => {
      console.error(error);
      return EMPTY;
    })
  );

  constructor(
    private timesheetService: TimesheetService,
    private modalService: MdbModalService
  ) {}

  openModal() {
    this.modalRef = this.modalService.open(NewTimesheetModalComponent);
    this.modalRef.onClose.subscribe((message: any) => {
      if (message) {
        this.timesheetService.onTimesheetAdded(message);
      }
    });
  }
}
