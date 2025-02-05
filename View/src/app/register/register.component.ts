import { Component, signal } from '@angular/core';
import {
  FormControl,
  FormGroupDirective,
  NgForm,
  Validators,
  FormsModule,
  ReactiveFormsModule,
  AbstractControl,
  ValidationErrors,
} from '@angular/forms';
import { ValidatorFn } from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatIconModule} from '@angular/material/icon';
import { Router } from '@angular/router';
import { UsersService } from '../services/users.service';


class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-register',
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
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  constructor(private router: Router, private userService: UsersService) {}
  emailFormControl = new FormControl<string>('', [Validators.required, Validators.email]);
  passwordFormControl = new FormControl<string>('', [Validators.required, Validators.minLength(6), Validators.pattern('.*(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*')]);
  confirmPasswordFormControl = new FormControl<string>('', [Validators.required, this.createPasswordMatchValidator()])
  matcher = new MyErrorStateMatcher();
  registrationError : boolean = false;
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
      };
      this.userService.registerUser(data).subscribe({
        next: (_response) => {
          this.loading = false;
          this.registrationError = false;
          this.router.navigate(['/']);
        },
        error: (_error) => {
          this.loading = false;
          this.registrationError = true;
        }
      })
    }
  }

  onBack(): void {
    this.router.navigate(['/login']);
  }

  createPasswordMatchValidator(): ValidatorFn {
    return (control:AbstractControl) : ValidationErrors | null => {
  
        const value = control.value;
  
        if (!value) {
            return null;
        }
  
        const passwordMatch = value == this.passwordFormControl.value;
  
        return !passwordMatch ? {passwordmatch:true}: null;
    }
  }
}
