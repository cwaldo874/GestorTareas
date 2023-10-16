import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Category } from '../model/category';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs';
import { environment } from 'src/enviroments/environment';
import { API_URL_CATEGORY } from 'src/constants/constants';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = environment.apiUrl + API_URL_CATEGORY;

  constructor(private http: HttpClient) { }

  getCategory(): Observable<Category[]> {
    var data = this.http.get(this.apiUrl, { headers: this.getToken() }).pipe(
      map(function (categoria: any) {
        return <Category[]>(categoria);
      },
      ))
    return data;
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