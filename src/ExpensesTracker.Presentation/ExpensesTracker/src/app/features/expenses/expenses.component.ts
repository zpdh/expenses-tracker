import {Component, OnInit} from '@angular/core';
import {GetExpensesResponse} from '../../core/communication/expenses/get/getExpensesResponse';
import {ExpensesService} from '../../core/services/expenses.service';
import {GetExpensesRequest} from '../../core/communication/expenses/get/getExpensesRequest';
import {CurrencyPipe, DatePipe, NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-expenses',
  imports: [
    NgIf,
    NgForOf,
    DatePipe,
    CurrencyPipe
  ],
  templateUrl: './expenses.component.html',
  standalone: true,
})
export class ExpensesComponent implements OnInit {
  private readonly expensesService: ExpensesService;

  protected expenses: GetExpensesResponse | null = null;
  protected errorMessage = '';

  protected request: GetExpensesRequest = {
    filter: '',
    since: new Date(0),
  }

  constructor(expensesService: ExpensesService) {
    this.expensesService = expensesService;
  }

  ngOnInit(): void {
    this.loadExpenses();
  }

  protected loadExpenses() {
    this.expensesService.getExpenses(this.request).subscribe({
      next: (res) => this.expenses = res,
      error: (err) => this.errorMessage = "Failed to load expenses.",
    });
  }

  protected deleteExpense(expenseId: number) {
    if (confirm("Are you sure you want to delete this expense?")) {
      this.expensesService.deleteExpense(expenseId).subscribe({
        next: () => this.expenses?.filter(exp => exp.id !== expenseId),
        error: (err: Error) => this.errorMessage = "Failed to delete expense"
      });
    }
  }
}
