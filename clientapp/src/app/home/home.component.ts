import { Component, OnInit } from "@angular/core";
import { AuthService } from "@app/core/service/auth.service";
import { Timesheet } from "@app/data/schema/timesheet";
import { TimesheetService } from "@app/data/service/timesheet.service";
import { MdbModalRef, MdbModalService } from "mdb-angular-ui-kit/modal";
import { NewTimesheetModalComponent } from "./new-timesheet-modal.component";

@Component({
  selector: "home",
  templateUrl: "home.component.html",
})
export class HomeComponent implements OnInit {
  modalRef: MdbModalRef<NewTimesheetModalComponent> | null = null;

  constructor(private timesheetService: TimesheetService, private modalService: MdbModalService) {}

  ngOnInit(): void {
    this.timesheetService.getAll().subscribe({
      next: (data: Timesheet[]) => {
        console.log(data);
      },
    });
  }

  openModal() {
    this.modalRef = this.modalService.open(NewTimesheetModalComponent);
  }
}
