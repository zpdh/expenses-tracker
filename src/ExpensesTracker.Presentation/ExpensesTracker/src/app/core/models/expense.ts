export interface Expense {
  id: number,
  categoryId: number,
  userId: number,
  name: string,
  price: number,
  insertionDate: Date,
}
