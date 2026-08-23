import { Routes } from '@angular/router';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { NotFoundPageComponent } from './components/not-found-page/not-found-page.component';
import { TransactionListComponent } from './components/transaction-lists/transaction-list/transaction-list.component';
import { DashboardScreenComponent } from './components/dashboard/dashboard-screen/dashboard-screen.component';
import { GigDetailScreenComponent } from './components/gig-detail/gig-detail-screen/gig-detail-screen.component';
import { ProfileScreenComponent } from './components/profile-screen/profile-screen.component';
import { TransactionDetailComponent } from './components/transaction-detail/transaction-detail.component';
import { authGuard } from './guards/auth.guard';
import { GigCreateScreenComponent } from './components/gig-create/gig-create-screen/gig-create-screen.component';
import { GigDetailRatingCommentComponent } from './components/gig-detail/gig-detail-rating-comment/gig-detail-rating-comment.component';
import { GigEditComponent } from './components/gig-detail/gig-edit/gig-edit.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginPageComponent },
  { path: 'not-found', component: NotFoundPageComponent },
  {
    path: 'transactions',
    canActivate: [authGuard],
    component: TransactionListComponent,
  },
  {
    path: 'transactions/detail/:id',
    canActivate: [authGuard],
    component: TransactionDetailComponent,
  },
  {
    path: 'dashboard',
    component: DashboardScreenComponent,
  },
  {
    path: 'gig/create',
    component: GigCreateScreenComponent,
  },
  {
    path: 'gig/:gigId',
    canActivate: [authGuard],
    component: GigDetailScreenComponent,
    children: [
    
      {
        path:'edit',
        component: GigEditComponent,
      }
    ],
  },
  {
    path: 'profile/:id',
    canActivate: [authGuard],
    component: ProfileScreenComponent,
  },
  { path: '**', redirectTo: '/not-found' },
];
