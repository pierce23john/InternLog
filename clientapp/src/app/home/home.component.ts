import { AfterViewInit, Component, ViewChild } from "@angular/core";
import { GetTimesheetResponse } from "@app/data/models/timesheet";
import { NewTimesheetModalComponent } from "./new-timesheet-modal.component";
import { FormGroup, FormControl } from "@angular/forms";
import { map } from "rxjs";
import { MatDialog, MatDialogRef } from "@angular/material/dialog";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "home",
  templateUrl: "home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements AfterViewInit {
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

  timesheets$ = this.activatedRoute.data.pipe(
    map((data) => <GetTimesheetResponse[]>data.timesheets)
  );

  displayedColumns: string[] = [
    "date",
    "day",
    "timeIn",
    "timeOut",
    "hoursRendered",
    "hoursRenderedMinusLunchBreak",
  ];

  constructor(
    public dialog: MatDialog,
    private activatedRoute: ActivatedRoute
  ) {}

  ngAfterViewInit(): void {
    this.timesheets$.subscribe({
      next: (value) => {
        console.log(value);
        this.dataSource.data = value;
      },
    });
    this.dataSource.paginator = this.paginator;
  }

  openModal() {
    // this.modalRef = this.modalService.open(NewTimesheetModalComponent);
    // this.modalRef.onClose.subscribe((message: any) => {
    //   if (message) {
    //     this.timesheetService.onTimesheetAdded(message);
    //   }
    // });
  }
}
