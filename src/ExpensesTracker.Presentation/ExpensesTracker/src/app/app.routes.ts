import {Routes} from '@angular/router';
import {LoginComponent} from './features/login/login.component';
import {ExpensesComponent} from './features/expenses/expenses.component';
import {authGuard} from './core/auth/auth.guard';
import {RegisterComponent} from './features/register/register.component';
import {ManageAccountComponent} from './features/account/manage-account/manage-account.component';
import {SearchComponent} from './features/expenses/search/search.component';

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
    path: 'account',
    component: ManageAccountComponent,
    canActivate: [authGuard]
  },
  {
    path: 'search',
    component: SearchComponent,
    canActivate: [authGuard]
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: '**',
    redirectTo: 'expenses'
  }
];
