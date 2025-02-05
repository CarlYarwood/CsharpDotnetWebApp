import { Component } from '@angular/core';
import { UserCareTeam, UserCareTeamService } from '../services/user-care-team.service';
import { CareTeam, CareTeamService } from '../services/care-team.service';
import { Patient, PatientService } from '../services/patient.service';
import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatSelectModule} from '@angular/material/select';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {FormsModule} from '@angular/forms';
import { ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-home',
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatSelectModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent {
  constructor(private userCareTeamService: UserCareTeamService, private careTeamService: CareTeamService, private patientService: PatientService, private cd: ChangeDetectorRef) {}
  userCareTeams : UserCareTeam[]  = [];
  careTeams : CareTeam[] = [];
  patients : Patient[] = [];
  hasErrored: boolean = false;
  firstName = '';
  lastName = '';
  displayedColumns: string[] = ["firstName", "lastName"];

  ngOnInit() {
    this.userCareTeamService.getUserCareTeams().subscribe({
      next: (response: UserCareTeam[]) => {
        this.userCareTeams = response
        this.getCareTeams()
      },
      error: (_error) => {
        this.hasErrored = true;
      }
    })
  }

  getCareTeams() {
    this.userCareTeams.forEach((uct: UserCareTeam) => {
      this.careTeamService.getCareTeamById(uct.careTeamId).subscribe({
        next: (response: CareTeam) => {
          this.careTeams.push(response);
        },
        error: (_error) => {
          this.hasErrored = true;
        }
      });
    });
    this.getAllPatients();
  }

  getAllPatients() {
    this.careTeams.forEach((ct: CareTeam) => {
      this.patientService.getPatientsByCareTeamId(ct.id).subscribe({
        next: (response: Patient[]) => { 
          console.log(response);
          this.patients = this.patients.concat(response);
        },
        error: (_error) => {
          this.hasErrored = true;
        }
      })
    });
    this.cd.markForCheck();
  }

  onFilter() {}
 }
