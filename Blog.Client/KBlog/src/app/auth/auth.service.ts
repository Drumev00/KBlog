import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { User } from './user.model';
import { environment } from 'src/environments/environment';

interface RegisterUserRequest {
  username: string;
  email: string;
  password: string;
  repeatPassword: string;
}

interface LoginRequest {
  username: string;
  password: string;
}

interface LoginResponse {
  jwtToken: string;
  expirationTime: Date;
  errors: Array<string>;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  loggedUser = new BehaviorSubject<User>(null);
  registerPath: string = `${environment.apiRoute}/identity/register`;
  loginPath: string = `${environment.apiRoute}/identity/login`;

  constructor(private http: HttpClient, private router: Router) {}

  public get userValue(): User {
    return this.loggedUser.value;
  }

  registerUser(body: RegisterUserRequest) {
    return this.http.post(this.registerPath, body, {
      responseType: 'text',
    });
  }

  login(body: LoginRequest) {
    return this.http.post<LoginResponse>(this.loginPath, body);
  }
}
