import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

export interface CareTeam {
  id: number,
  leadId: string, 
  name: string
}

@Injectable({
  providedIn: 'root'
})
export class CareTeamService {

  constructor(private http: HttpClient) {}

  getCareTeamById(id: number) {
    const bearer = sessionStorage.getItem("Bearer_Token");
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + bearer,
      'Content-Type': 'application/json'
    });
    return this.http.get<CareTeam>("http://localhost:5200/api/CareTeams/" + String(id), {headers});
  }
}
