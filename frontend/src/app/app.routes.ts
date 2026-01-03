import { Routes } from '@angular/router';
import { AuthLayout } from './layouts/auth-layout/auth-layout';
import { Register } from './features/auth/register/register';
import { Login } from './features/auth/login/login';
import { VerifyEmail } from './features/auth/verify-email/verify-email';

export const routes: Routes = [
  { path: '', redirectTo: 'auth/register', pathMatch: 'full' },
  {
    path: 'auth',
    component: AuthLayout,
    children: [
      { path: 'register', component: Register },
      { path: 'login', component: Login },
      { path: 'verify-email', component: VerifyEmail },
      { path: '', redirectTo: 'register', pathMatch: 'full' },
    ]
  }
];
