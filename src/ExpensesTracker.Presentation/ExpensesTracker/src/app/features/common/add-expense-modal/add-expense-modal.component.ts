import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {AddExpenseRequest} from '../../../core/communication/expenses/add/addExpenseRequest';
import {GetCategoriesResponse} from '../../../core/communication/categories/getCategoriesResponse';
import {CategoryService} from '../../../core/services/category.service';
import {ExpensesService} from '../../../core/services/expenses.service';
import {FormsModule} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';
import {Category} from '../../../core/models/category';

@Component({
  selector: 'app-add-expense-modal',
  imports: [
    FormsModule,
    NgForOf,
    NgIf
  ],
  templateUrl: './add-expense-modal.component.html',
  standalone: true,
})
export class AddExpenseModalComponent implements OnInit {
  private readonly categoryService: CategoryService;
  private readonly expensesService: ExpensesService;

  @Output() expenseAdded = new EventEmitter<void>();
  @Output() closed = new EventEmitter<void>();

  protected errorMessage = "";
  protected categories: Category[] = [];
  protected newExpense: AddExpenseRequest = {
    categoryId: 0,
    name: '',
    price: 0,
  }

  constructor(categoryService: CategoryService, expensesService: ExpensesService) {
    this.categoryService = categoryService;
    this.expensesService = expensesService;
  }

  ngOnInit(): void {
    this.loadCategories();
  }

  protected loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: (cats) => {
        this.categories = cats.categories;

        if (this.categories.length > 0) {
          this.newExpense.categoryId = this.categories[0].id;
        }
      },
      error: (err: Error) => this.errorMessage = `Failed to load categories. Error: ${err.message}`
    });
  }

  protected onSubmit() {
    this.expensesService.addExpense(this.newExpense).subscribe({
      next: () => this.expenseAdded.emit(),
      error: (err: Error) => this.errorMessage = `Failed to add expense. Error: ${err.message}`
    });
  }

  close() {
    this.closed.emit();
  }
}

