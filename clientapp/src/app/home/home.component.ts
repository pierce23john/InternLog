import { Component, OnInit } from "@angular/core";
import { GetTimesheetResponse } from "@app/data/models/getTimesheetResponse";
import { TimesheetService } from "@app/data/service/timesheet.service";
import { MdbModalRef, MdbModalService } from "mdb-angular-ui-kit/modal";
import { NewTimesheetModalComponent } from "./new-timesheet-modal.component";
import { FormGroup, FormControl } from "@angular/forms";

@Component({
  selector: "home",
  templateUrl: "home.component.html",
})
export class HomeComponent implements OnInit {
  modalRef: MdbModalRef<NewTimesheetModalComponent> | null = null;

  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  public timesheets: GetTimesheetResponse[] = [];

  constructor(
    private timesheetService: TimesheetService,
    private modalService: MdbModalService
  ) {}

  ngOnInit(): void {
    this.loadTimesheets();
  }

  openModal() {
    this.modalRef = this.modalService.open(NewTimesheetModalComponent);
    this.modalRef.onClose.subscribe((message: any) => {
      if (Boolean(message)) {
        this.loadTimesheets();
      }
    });
  }

  loadTimesheets() {
    this.timesheetService.getAll().subscribe({
      next: (data: GetTimesheetResponse[]) => {
        this.timesheets = data;
      },
    });
  }
}
