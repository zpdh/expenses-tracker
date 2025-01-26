import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GetCategoriesResponse} from '../communication/categories/getCategoriesResponse';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private readonly apiUrl = "http://localhost:5124/api/category";
  private readonly httpClient: HttpClient;

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  getCategories(): Observable<GetCategoriesResponse> {
    return this.httpClient.get<GetCategoriesResponse>(this.apiUrl);
  }
}
