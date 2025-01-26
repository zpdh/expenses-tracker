import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GetUserByIdResponse} from '../communication/user/get/getUserByIdResponse';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly httpClient: HttpClient;
  private readonly apiUrl = "http://localhost:5124/api/user";

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  getUser(): Observable<GetUserByIdResponse> {
    return this.httpClient.get<GetUserByIdResponse>(this.apiUrl);
  }

  deleteUser(): Observable<void> {
    return this.httpClient.delete<void>(this.apiUrl);
  }
}
