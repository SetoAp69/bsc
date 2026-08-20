import { Routes } from '@angular/router';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { NotFoundPageComponent } from './components/not-found-page/not-found-page.component';
import { TransactionListComponent } from './components/transaction-list/transaction-list.component';
import { DashboardScreenComponent } from './component/dashboard/dashboard-screen/dashboard-screen.component';
import { GigDetailScreenComponent } from './component/gig-detail-screen/gig-detail-screen.component';

export const routes: Routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: 'login', component: LoginPageComponent },
    { path: 'not-found', component: NotFoundPageComponent },
    { path: 'transactions/:userId', component: TransactionListComponent },
    {
    path: 'dashboard',
    component: DashboardScreenComponent,
    },
    {
      path: 'gig/:id',
      component: GigDetailScreenComponent,
    },
    { path: '**', redirectTo: '/not-found' }
