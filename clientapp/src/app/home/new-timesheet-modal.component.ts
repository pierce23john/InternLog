import { Component, OnInit } from '@angular/core';
import { MdbModalRef } from 'mdb-angular-ui-kit/modal';

@Component({
    selector: 'new-timesheet-modal',
    templateUrl: 'new-timesheet-modal.component.html'
})

export class NewTimesheetModalComponent implements OnInit {

    constructor(public modalRef: MdbModalRef<NewTimesheetModalComponent>) {}

    ngOnInit() { }
}