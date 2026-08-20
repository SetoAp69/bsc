import { Routes } from '@angular/router';
import { DashboardScreenComponent } from './component/dashboard/dashboard-screen/dashboard-screen.component';
import { GigDetailScreenComponent } from './component/gig-detail-screen/gig-detail-screen.component';

export const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardScreenComponent,
  },
  {
    path: 'gig/:id',
    component: GigDetailScreenComponent,
  },
];
