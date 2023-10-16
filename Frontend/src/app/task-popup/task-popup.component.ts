import { Component, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-task-popup',
  templateUrl: './task-popup.component.html',
  styleUrls: ['./task-popup.component.css']
})
export class TaskPopupComponent implements OnInit {

  modal: NgbModalRef;
  ngOnInit() {
  }

  DeleteTask() {
    this.modal.close();
  }
  deleteCancel() {
    this.modal.dismiss();
  }
}
