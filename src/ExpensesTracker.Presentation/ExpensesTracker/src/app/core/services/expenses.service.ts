import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GetExpensesResponse} from '../communication/expenses/get/getExpensesResponse';
import {GetExpensesRequest} from '../communication/expenses/get/getExpensesRequest';
import {AddExpenseRequest} from '../communication/expenses/add/addExpenseRequest';

@Injectable({
  providedIn: 'root'
})
export class ExpensesService {
  private readonly apiUrl = "http://localhost:5124/api/expenses";
  private readonly httpClient: HttpClient;

  constructor(client: HttpClient) {
    this.httpClient = client;
  }

  public getExpenses(request: GetExpensesRequest): Observable<GetExpensesResponse> {
    return this.httpClient.get<GetExpensesResponse>(this.apiUrl, {
      params: {
        filter: request.filter,
        since: request.since.toISOString()
      }
    });
  }

  public addExpense(request: AddExpenseRequest): Observable<void> {
    return this.httpClient.post<void>(this.apiUrl, request);
  }

  public deleteExpense(expenseId: number): Observable<void> {
    return this.httpClient.delete<void>(this.apiUrl, {
      body: {expenseId}
    });
  }
}
