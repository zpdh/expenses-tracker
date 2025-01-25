import {HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {AuthService} from '../../auth/auth.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.getToken();

  if (token) {
    const cloned = req.clone({
      setHeaders: {Authorization: `Bearer ${token}`}
    });
    return next(cloned);
  }

  return next(req);
}
