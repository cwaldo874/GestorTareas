import { Component, OnInit } from '@angular/core';
import { TaskService } from '../services/task.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Task } from '../model/task';
import { CategoryService } from '../services/category.service';
import { Category } from '../model/category';
import { ModalTaskFormComponent } from '../modal-task-form/modal-task-form.component';
import { TaskPopupComponent } from '../task-popup/task-popup.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  searchTerm: string = '';
  tasks: Task[] = [];
  originalTasks: Task[] = [];
  categories: Category[] = [];
  token: string = window.localStorage["Token"];

  constructor(private taskService: TaskService, private modalService: NgbModal, private categoryService: CategoryService, private router: Router) { }

  ngOnInit() {
    this.getTaskService();
    this.getCategoriesService();
  }

  editTask(task: Task) {
    const modalRef = this.modalService.open(ModalTaskFormComponent, { centered: true });
    modalRef.componentInstance.task = { ...task };
    modalRef.componentInstance.modal = modalRef;
    modalRef.result.then((data) => {
      this.updateTaskService(data);
    }, (reason) => {
      // on dismiss
    });
  }

  changeTaskState(task: Task) {
    task.completada = !task.completada;
    this.updateTaskService(task);
  }
  createTask() {
    const modalRef = this.modalService.open(ModalTaskFormComponent, { centered: true });
    modalRef.componentInstance.task = new Task;
    modalRef.componentInstance.modal = modalRef;
    modalRef.result.then((data) => {
      this.createTaskService(data);

    }, (reason) => {
      // on dismiss
    });
  }

  deleteTask(task: Task) {
    const modalRef = this.modalService.open(TaskPopupComponent, { centered: true });
    modalRef.componentInstance.modal = modalRef;
    modalRef.result.then(() => {
      this.deleteTaskService(task);
    }, (reason) => {
      // on dismiss
    });
  }

  filterTasks() {
    var categoria: any = this.searchTerm
    if (this.searchTerm != "") {
      this.tasks = this.originalTasks.filter(task =>
        task.categoria.nombre.toLowerCase().includes(categoria.nombre.toLowerCase())
      );
    }
    else {
      this.tasks = this.originalTasks
    }
  }

  getTaskService(): void {
    this.taskService.getTasks().subscribe((data: any) => {
      this.tasks = data;
      this.originalTasks = this.tasks;
    },
      (error: any) => {
        this.badRequestReturn();
      });
  }

  updateTaskService(task: Task): void {
    this.taskService.updateTask(task).subscribe(
      (data: any) => {
        this.getTaskService();
      },
      (error: any) => {
        this.badRequestReturn();
      }
    );
  }

  createTaskService(task: Task): void {
    this.taskService.createTask(task).subscribe(
      (data: any) => {
        this.getTaskService();
      }
    );
  }

  getCategoriesService(): void {
    this.categoryService.getCategory().subscribe((data: any) => {
      this.categories = data;
    });
  }

  deleteTaskService(task: Task): void {
    this.taskService.deleteTask(task).subscribe(
      (data: any) => {
        this.getTaskService();
      }
    );
  }

  badRequestReturn() {
    this.router.navigate(['login']);
  }
  sessionClose() {
    this.badRequestReturn();
    localStorage.setItem('Token', JSON.stringify(""));
  }
}

