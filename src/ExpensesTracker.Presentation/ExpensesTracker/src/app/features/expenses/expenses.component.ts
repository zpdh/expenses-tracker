import {Component, OnInit} from '@angular/core';
import {GetExpensesResponse} from '../../core/communication/expenses/get/getExpensesResponse';
import {ExpensesService} from '../../core/services/expenses.service';
import {GetExpensesRequest} from '../../core/communication/expenses/get/getExpensesRequest';

@Component({
  selector: 'app-expenses',
  imports: [],
  templateUrl: './expenses.component.html',
  standalone: true,
})
export class ExpensesComponent implements OnInit {
  private readonly expensesService: ExpensesService;

  protected expenses: GetExpensesResponse | null = null;
  protected errorMessage = '';

  protected request: GetExpensesRequest = {
    filter: '',
    since: new Date(Date.now() - 7 * 24 * 60 * 60 * 1000) // get current date - 7 days in milliseconds
  }

  constructor(expensesService: ExpensesService) {
    this.expensesService = expensesService;
  }

  ngOnInit(): void {
    this.loadExpenses();
  }

  private loadExpenses() {
    this.expensesService.getExpenses(this.request).subscribe({
      next: (res) => this.expenses = res,
      error: (err) => this.errorMessage = "Failed to load expenses.",
    });
  }
}
