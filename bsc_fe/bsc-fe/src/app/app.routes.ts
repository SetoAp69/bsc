import { Routes } from '@angular/router';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { NotFoundPageComponent } from './components/not-found-page/not-found-page.component';
import { TransactionListComponent } from './components/transaction-list/transaction-list.component';
import { DashboardScreenComponent } from './component/dashboard/dashboard-screen/dashboard-screen.component';
import { GigDetailScreenComponent } from './component/gig-detail-screen/gig-detail-screen.component';
import { ProfileScreenComponent } from './component/profile-screen/profile-screen.component';
import { GigsByUserScreenComponent } from './component/gigs-by-user-screen/gigs-by-user-screen.component';
import { OrderScreenComponent } from './component/order-screen/order-screen.component';
import { TransactionDetailComponent } from './components/transaction-detail/transaction-detail.component';
import { GigDetailRatingCommentComponent } from './component/gig-detail-rating-comment/gig-detail-rating-comment.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginPageComponent },
  { path: 'not-found', component: NotFoundPageComponent },
  {
    path: 'transactions',
    component: TransactionListComponent,
  },
  {
    path: 'transactions/detail/:id',
    component: TransactionDetailComponent,
  },
  {
    path: 'dashboard',
    component: DashboardScreenComponent,
  },
  {
    path: 'gig/:gigId',
    component: GigDetailScreenComponent,
    children: [
      {
        path: 'ratings',
        component: GigDetailRatingCommentComponent,
      },
    ],
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
  // { path: '**', redirectTo: '/not-found' }
];
