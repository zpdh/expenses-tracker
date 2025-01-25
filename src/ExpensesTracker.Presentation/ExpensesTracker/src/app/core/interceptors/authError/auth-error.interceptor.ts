import {HttpErrorResponse, HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {AuthService} from '../../auth/auth.service';
import {Router} from '@angular/router';
import {catchError, throwError} from 'rxjs';

export const authErrorInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);


  return next(req).pipe(catchError((err: HttpErrorResponse) => {
    if (err.status === 401) {
      authService.logout();
      router.navigate(["/login"]).then();
    }
    return throwError(() => err);
  }));
}
