import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { PatientComponent } from './patient/patient.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'patient/:id',
        component: PatientComponent,
        canActivate: [authGuard]
    },
    {
        path: '',
        component: HomeComponent,
        canActivate: [authGuard]
    },
    { path: '**',
        component: PageNotFoundComponent
    }
];
