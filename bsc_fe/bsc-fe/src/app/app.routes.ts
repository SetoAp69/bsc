import { Routes } from '@angular/router';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { NotFoundPageComponent } from './components/not-found-page/not-found-page.component';
import { TransactionListComponent } from './components/transaction-list/transaction-list.component';
import { DashboardScreenComponent } from './component/dashboard/dashboard-screen/dashboard-screen.component';
import { GigDetailScreenComponent } from './component/gig-detail-screen/gig-detail-screen.component';
import { ProfileScreenComponent } from './component/profile-screen/profile-screen.component';
import { GigsByUserScreenComponent } from './component/gigs-by-user-screen/gigs-by-user-screen.component';
import { OrderScreenComponent } from './component/order-screen/order-screen.component';

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
  {
    path: 'gig-by-user/:userId',
    component: GigsByUserScreenComponent,
  },
  {
    path: 'profile/:id',
    component: ProfileScreenComponent,
  },
  {
    path: 'order/:itemId/:paymentMethodId',
    component: OrderScreenComponent,
  },
  { path: '**', redirectTo: '/not-found' },
];
