import { DatePipe } from "@angular/common";
import {
  Component,
  EventEmitter,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from "@angular/core";
import { CreateTimesheetRequest } from "@app/data/models/createTimesheetRequest";
import { TimesheetService } from "@app/data/service/timesheet.service";
import { MdbModalRef } from "mdb-angular-ui-kit/modal";

@Component({
  selector: "new-timesheet-modal",
  templateUrl: "new-timesheet-modal.component.html",
})
export class NewTimesheetModalComponent implements OnInit, OnChanges {
  private datePipe: DatePipe;

  timesheet: CreateTimesheetRequest;
  constructor(
    public modalRef: MdbModalRef<NewTimesheetModalComponent>,
    private timesheetService: TimesheetService
  ) {
    this.datePipe = new DatePipe("en-PH");
  }
  ngOnChanges(changes: SimpleChanges): void {
    console.log({ changes });
  }

  ngOnInit() {
    this.timesheet = new CreateTimesheetRequest();
    this.timesheet.date = new Date();
    this.timesheet.timeIn = this.datePipe.transform(new Date(), "HH:mm");
    this.timesheet.timeOut = this.datePipe.transform(new Date(), "HH:mm");

    console.log(this.timesheet);
  }

  saveTimesheet() {
    this.timesheetService.create(this.timesheet).subscribe({
      complete: () => {
        this.modalRef.close(true);
      },
    });
  }
}
