import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpParams,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private jwtHelper: JwtHelperService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = localStorage.getItem('jwt');

    if (!token) {
      return next.handle(req);
    } else if (token && this.jwtHelper.isTokenExpired(token)) {
      // auth service to /identity/refreshToken
    }
    const modifiedRequest = req.clone({
      params: new HttpParams().set('auth', token),
    });

    return next.handle(modifiedRequest);
  }
}
