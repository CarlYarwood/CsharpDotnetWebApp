import { Component } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { Interceptor } from './interceptor';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    RouterModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: Interceptor,
    multi: true
  }],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {}
