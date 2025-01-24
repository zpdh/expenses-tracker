import {Expense} from '../../../models/expense';

export interface GetExpensesResponse {
  expenses: Expense[],
}
