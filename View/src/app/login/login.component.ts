import { Component, signal } from '@angular/core';
import {
  FormControl,
  FormGroupDirective,
  NgForm,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatIconModule} from '@angular/material/icon';
import { Router } from '@angular/router';
import { LoginResponse, LoginService } from '../services/login.service';
import { UserResponse, UsersService } from '../services/users.service';

class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-login',
  imports: [
    MatCardModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatGridListModule,
    MatIconModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private router: Router, private loginService: LoginService, private userService: UsersService) {}
  emailFormControl = new FormControl<string>('', [Validators.required, Validators.email]);
  passwordFormControl = new FormControl<string>('', [Validators.required]);
  matcher = new MyErrorStateMatcher();
  loginError : boolean = false;
  loading : boolean = false;
  hidePass = signal(true);

  toggleHide(): void {
    this.hidePass.set(!this.hidePass())
  }

  onSubmit(): void {
    if (this.emailFormControl.valid && this.passwordFormControl.valid) {
      this.loading = true;
      var data = {
        "email": this.emailFormControl.value,
        "password": this.passwordFormControl.value,
        "useCookie": true
      };
      this.loginService.login(data).subscribe(
        {
          next: (response:LoginResponse) => {
            sessionStorage.setItem("Bearer_Token", response.accessToken);
            sessionStorage.setItem("Refresh_Token", response.refreshToken);
            this.getUserInfo();
          },
          error: (_err: any) => {
            this.loading = false;
            this.loginError = true;
          }
        }
      );
    }
  }

  getUserInfo(): void {
    const bearer = sessionStorage.getItem("Bearer_Token");
    if (bearer != null) {
      this.userService.getUser().subscribe({
        next: (response:UserResponse) => {
          this.loading = false;
          this.loginError = false;
          sessionStorage.setItem("User_Id", response.userId);
          sessionStorage.setItem("User_Email", response.userEmail);
          sessionStorage.setItem("User_Role", response.userRole)
          this.router.navigate(['/']);
        },
        error: (_err: any) => {
          this.loading = false;
          this.loginError = true;
        }
      });
    }
    else {
      this.loading = false;
      this.loginError = true;
    }
  }

  onRegister(): void {
    this.router.navigate(['/register'])
  }
}
