import {Routes} from '@angular/router';
import {LoginComponent} from './features/login/login.component';
import {ExpensesComponent} from './features/expenses/expenses.component';
import {authGuard} from './core/auth/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'expenses',
    pathMatch: 'full'
  },
  {
    path: 'expenses',
    component: ExpensesComponent,
    canActivate: [authGuard]
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: '**',
    redirectTo: 'expenses'
  }
];
