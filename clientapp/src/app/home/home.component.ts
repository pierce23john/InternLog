import { AfterViewInit, Component, OnInit, ViewChild } from "@angular/core";
import { GetTimesheetResponse } from "@app/data/models/timesheet";
import { TimesheetService } from "@app/data/service/timesheet.service";
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
  Observable,
} from "rxjs";
import { Timesheet } from "@app/data/schema/timesheet";
import { AuthService } from "@app/core/service/auth.service";
import { MatDialog, MatDialogRef } from "@angular/material/dialog";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "home",
  templateUrl: "home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit, AfterViewInit {
  dialogRef: MatDialogRef<NewTimesheetModalComponent> | null = null;

  dataSource = new MatTableDataSource<GetTimesheetResponse>();

  @ViewChild(MatPaginator) paginator: MatPaginator;

  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  openDialog(): void {
    this.dialogRef = this.dialog.open(NewTimesheetModalComponent, {
      width: "450px",
      height: "auto",
    });
  }

  rangeFilters$ = combineLatest([
    this.range.controls.start.valueChanges.pipe(filter((d) => Boolean(d))),
    this.range.controls.end.valueChanges.pipe(filter((d) => Boolean(d))),
  ]).pipe(
    filter(([start, end]) => Boolean(start) && Boolean(end)),
    map(([start, end]) => ({ start, end }))
  );

  timesheetsWithAdd$ = combineLatest([
    this.activatedRoute.data.pipe(
      map((data) => <GetTimesheetResponse[]>data["timesheets"])
    ),
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

  // userData$ = this.authService.userData$;

  displayedColumns: string[] = [
    "date",
    "day",
    "timeIn",
    "timeOut",
    "hoursRendered",
    "hoursRenderedMinusLunchBreak",
  ];

  constructor(
    private timesheetService: TimesheetService,
    private authService: AuthService,
    public dialog: MatDialog,
    private activatedRoute: ActivatedRoute
  ) {}

  ngAfterViewInit(): void {
    this.timesheetsWithAdd$.subscribe((data) => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
    });
  }
  ngOnInit(): void {}

  openModal() {
    // this.modalRef = this.modalService.open(NewTimesheetModalComponent);
    // this.modalRef.onClose.subscribe((message: any) => {
    //   if (message) {
    //     this.timesheetService.onTimesheetAdded(message);
    //   }
    // });
  }
}
