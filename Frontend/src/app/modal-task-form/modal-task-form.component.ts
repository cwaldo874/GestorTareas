import { Component, Input, OnInit } from '@angular/core';
import { Task } from '../model/task';
import { CategoryService } from '../services/category.service';
import { Category } from '../model/category';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-task-form',
  templateUrl: './modal-task-form.component.html',
  styleUrls: ['./modal-task-form.component.css']
})
export class ModalTaskFormComponent implements OnInit {
  @Input()
  task: Task;
  taskOriginal: Task;
  categories: Category[] = [];
  modal: NgbModalRef;
  InvalidateDate = false;
  invalidateDescription = false;
  invalidateName = false;
  invalidateCategory = false;


  constructor(private categoryService: CategoryService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.categoryService.getCategory().subscribe((data: any) => {
      this.categories = data;
    });
    this.taskOriginal = { ...this.task };

  }

  onChangeDate(value: Date) {
    if (!value || value.toString() === 'Invalid Date') {
      this.InvalidateDate = true;
    } else {
      this.InvalidateDate = false;
      this.task.fechaTarea = value;
    }
  }

  DateFormat(fecha: Date): string {
    if (fecha != undefined) {
      return fecha.toString().substring(0, 10);
    }
    return "";
  }

  onChangeTaskState(value: any) {
    this.task.completada = value.target.checked
  }

  categoryCompare(categoria1: Category, categoria2: Category) {
    if (categoria1 == null || categoria2 == null) {
      return false;
    }
    return categoria1.id === categoria2.id;
  }
  saveEdit() {
    if (this.task.nombre == undefined || this.task.nombre == "") {
      this.invalidateName = true;
      return;
    }
    else {
      this.invalidateName = false;
    }
    if (this.task.descripcion == undefined || this.task.descripcion == "") {
      this.invalidateDescription = true;
      return;
    }
    else {
      this.invalidateDescription = false;
    }
    this.onChangeDate(this.task.fechaTarea);
    if (this.InvalidateDate == true)
      return
    if (this.task.categoria == undefined) {
      this.invalidateCategory = true;
      return;
    } else {
      this.invalidateCategory = false;
    }
    this.modal.close(this.task);
  }

  cancelEdit() {
    this.modal.dismiss();
  }

  getToday(): string {
    const today = new Date();
    const day = today.getDate();
    const month = today.getMonth() + 1;
    const year = today.getFullYear();
    return `${year}-${month.toString().padStart(2, '0')}-${day.toString().padStart(2, '0')}`;
}
}
