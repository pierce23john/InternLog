import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { timeValidator } from "@app/core/validators/timeValidator";
import {
  CreateTimesheetRequest,
  CreateTimesheetResponse,
} from "@app/data/models/timesheet";

import { TimesheetService } from "@app/data/service/timesheet.service";
import { createDateFromTimeString } from "@app/shared/helpers/timeHelpers";

@Component({
  selector: "new-timesheet-modal",
  templateUrl: "new-timesheet-modal.component.html",
  styleUrls: ["./new-timesheet-modal.component.scss"],
})
export class NewTimesheetModalComponent implements OnInit {
  private datePipe: DatePipe;

  onNoClick(): void {
    this.dialogRef.close();
  }

  date = new FormControl(new Date());
  timeIn = new FormControl("");
  timeOut = new FormControl("");
  holidayOrAbsent = new FormControl("");

  timesheetForm = new FormGroup(
    {
      date: this.date,
      timeIn: this.timeIn,
      timeOut: this.timeOut,
      holidayOrAbsent: this.holidayOrAbsent,
    },
    { validators: [timeValidator("timeIn", "timeOut")] }
  );

  constructor(
    private timesheetService: TimesheetService,
    public dialogRef: MatDialogRef<NewTimesheetModalComponent>
  ) {
    this.datePipe = new DatePipe("en-PH");
  }

  ngOnInit() {
    this.timeIn.setValue(this.datePipe.transform(new Date(), "HH:mm"));
    this.timeOut.setValue(this.datePipe.transform(new Date(), "HH:mm"));
  }

  saveTimesheet() {
    console.log(this.timesheetForm.value);
    const request: CreateTimesheetRequest = {
      timeIn: createDateFromTimeString(this.timeIn.value),
      timeOut: createDateFromTimeString(this.timeOut.value),
      date: new Date(this.date.value),
      isHoliday: this.holidayOrAbsent.value === "holiday",
      isAbsent: this.holidayOrAbsent.value === "absent",
    };

    let response: CreateTimesheetResponse;

    this.timesheetService.create(request).subscribe({
      next: (data) => {
        response = data;
      },
      error: (error) => {
        console.error(error);
      },
      complete: () => {},
    });
  }
}
