import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

export interface Patient {
  id: number,
  firstName: string,
  lastName: string,
  careTeamId: number
}

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  constructor(private http: HttpClient) {}

  getPatientsByCareTeamId(id: number)
  {
    const bearer = sessionStorage.getItem("Bearer_Token");
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + bearer,
      'Content-Type': 'application/json'
    });
    return this.http.get<Patient[]>("http://localhost:5200/api/Patients?careTeamId=" + String(id), { headers });
  } 
}
