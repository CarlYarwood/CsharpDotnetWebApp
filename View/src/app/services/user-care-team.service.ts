import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

export interface UserCareTeam {
  id: number,
  userId: string,
  careTeamId: number
}

@Injectable({
  providedIn: 'root'
})
export class UserCareTeamService {

  constructor(private http: HttpClient) {}

  getUserCareTeams() {
    const bearer = sessionStorage.getItem("Bearer_Token");
    const userId = sessionStorage.getItem("User_Id");
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + bearer,
      'Content-Type': 'application/json'
    });
    return this.http.get<UserCareTeam[]>("http://localhost:5200/api/UserCareTeams?userId=" + userId, {headers})
  }
}