import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

export interface UserResponse {
  userId: string,
  userEmail: string,
  userRole: string
}

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient ) {}

  getUser() {
    const bearer = sessionStorage.getItem("Bearer_Token");
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + bearer,
      'Content-Type': 'application/json'
    });
    return this.http.get<UserResponse>('http://localhost:5200/api/Users', { headers });
  }

  registerUser(data: any) {
    return this.http.post('http://localhost:5200/api/Users', data);
  }
}
