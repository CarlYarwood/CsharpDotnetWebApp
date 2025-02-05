import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface LoginResponse{
  accessToken: string,
  refreshToken: string
}

@Injectable({
  providedIn: 'root'
})

export class LoginService {

  constructor(private http: HttpClient) {}

  login(data: any){
    return this.http.post<LoginResponse>('http://localhost:5200/login', data);
  }

  refresh(token: string){
    const data = {
      "refreshToken": token
    };
    return this.http.post<LoginResponse>('http://localhsot:5200/refresh', data);
  }
}
