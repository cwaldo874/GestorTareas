import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Task } from '../model/task';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs';
import { environment } from 'src/enviroments/environment';
import { API_URL_TASK } from 'src/constants/constants';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = environment.apiUrl + API_URL_TASK;

  constructor(private http: HttpClient) { }

  getTasks(): Observable<Task[]> {
    console.log(localStorage.getItem('Token'));
    var data = this.http.get(this.apiUrl, { headers: this.getToken() }).pipe(
      map(function (task: any) {
        return <Task[]>(task);
      },
      ))
    return data;
  }

  updateTask(task: Task): Observable<object> {
    return this.http.put(this.apiUrl + "/" + task.id, task, { headers: this.getToken() });
  }
  createTask(task: Task): Observable<object> {
    return this.http.post(this.apiUrl, task, { headers: this.getToken() });
  }

  deleteTask(task: Task): Observable<object> {
    return this.http.delete(this.apiUrl + "/" + task.id, { headers: this.getToken() });
  }

  getToken(): HttpHeaders {
    var retrievedObject = localStorage.getItem('Token');
    var token: any;
    if (retrievedObject !== null) {
      token = JSON.parse(retrievedObject);
    }
    let headers = new HttpHeaders().set('Authorization', 'Bearer ' + token.token);
    return headers;
  }
}