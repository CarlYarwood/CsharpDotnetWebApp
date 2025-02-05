import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, Observable, of, throwError } from 'rxjs';
import { LoginResponse, LoginService } from './services/login.service';

@Injectable()
export class Interceptor implements HttpInterceptor {
    constructor(private inject: Injector, private router: Router) {}
    ctr = 0

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        return next.handle(req).pipe(catchError(x => this.handleAuthError(x)));
    }

    private handleAuthError(err: HttpErrorResponse): Observable<any> {
        let token = sessionStorage.getItem("Refresh_Token");
        if (err && err.status == 401 && this.ctr != 1 && token != null) {
            this.ctr ++;
            let service = this.inject.get(LoginService);
            service.refresh(token).subscribe({
                next: (response:LoginResponse) => {
                    sessionStorage.setItem("Bearer_Token", response.accessToken);
                    sessionStorage.setItem("Refresh_Token", response.refreshToken);
                    return of("Action token refreshed please try again");
                },
                error:  (err:any) => {
                    this.router.navigateByUrl('/');
                    return of(err.message);
                }
            });
            return of("Attempting to refresh token");
        }
        else {
            this.ctr = 0
            return throwError(() => new Error("Non Authentication Error"));
        }
    }
}