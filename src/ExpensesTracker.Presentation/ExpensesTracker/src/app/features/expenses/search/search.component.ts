import {Component} from '@angular/core';
import {ExpensesService} from '../../../core/services/expenses.service';
import {FormBuilder, ReactiveFormsModule} from '@angular/forms';
import {GetExpensesRequest} from '../../../core/communication/expenses/get/getExpensesRequest';
import {GetExpensesResponse} from '../../../core/communication/expenses/get/getExpensesResponse';
import {CurrencyPipe, DatePipe, NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-search',
  imports: [
    ReactiveFormsModule,
    NgForOf,
    NgIf,
    DatePipe,
    CurrencyPipe
  ],
  templateUrl: './search.component.html',
  standalone: true,
  styleUrl: './search.component.css'
})
export class SearchComponent {
  private readonly expensesService: ExpensesService;
  private readonly formBuilder: FormBuilder;

  protected searchForm;
  protected results: GetExpensesResponse = [];
  protected errorMessage = "";

  protected datePresets = [
    {label: "Last 7 Days", days: 7},
    {label: "Last 30 Days", days: 30},
    {label: "All Time", days: 0}
  ];

  constructor(expensesService: ExpensesService, formBuilder: FormBuilder) {
    this.expensesService = expensesService;
    this.formBuilder = formBuilder;

    this.searchForm = this.formBuilder.group({
      filter: [""],
      since: this.getDefaultDate()
    });
  }

  search() {
    const formValue = this.searchForm.value;
    const request: GetExpensesRequest = {
      filter: formValue.filter || "",
      since: formValue.since
        ? new Date(formValue.since)
        : new Date(0)
    }

    this.expensesService.getExpenses(request).subscribe({
      next: (expenses) => this.results = expenses,
      error: (err: Error) => this.errorMessage = `Failed to load results. Error: ${err.message}`
    });
  }

  setDatePreset(days: number) {
    const since = days > 0
      ? new Date(new Date().setDate(new Date().getDate() - days))
      : new Date(0);

    this.searchForm.patchValue({since});
    this.search();
  }

  getDefaultDate(): Date {
    const date = new Date();
    date.setDate(date.getDate() - 7);

    return date;
  }
}
