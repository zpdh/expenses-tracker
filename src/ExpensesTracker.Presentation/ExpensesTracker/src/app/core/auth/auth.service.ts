import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LoginRequest} from '../communication/login/loginRequest';
import {LoginResponse} from '../communication/login/loginResponse';
import {Observable, tap} from 'rxjs';
import {RegisterRequest} from '../communication/register/registerRequest';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiUrl = "http://localhost:5124/api/user";
  private readonly httpClient: HttpClient;
  private tokenKey = "auth_token";

  constructor(client: HttpClient) {
    this.httpClient = client;
  }

  public getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  public setToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
  }

  public login(request: LoginRequest): Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>(`${this.apiUrl}/login`, request)
      .pipe(
        tap(
          (res) => this.setToken(res.token)
        ));
  }

  public register(request: RegisterRequest): Observable<void> {
    return this.httpClient.post<void>(this.apiUrl, request);
  }

  public logout() {
    localStorage.removeItem(this.tokenKey);
  }
}
