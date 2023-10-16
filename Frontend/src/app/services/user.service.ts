import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs';
import { User } from '../model/user';
import { environment } from 'src/enviroments/environment';
import { API_URL_USER } from 'src/constants/constants';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl + API_URL_USER;

  constructor(private http: HttpClient) { }

  getTokenUser(user: User): Observable<object> {
    return this.http.post(this.apiUrl, user);
  }

}