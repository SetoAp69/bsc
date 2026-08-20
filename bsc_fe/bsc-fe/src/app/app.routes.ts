import { Routes } from '@angular/router';
import { DashboardScreenComponent } from './component/dashboard/dashboard-screen/dashboard-screen.component';
import { GigDetailScreenComponent } from './component/gig-detail-screen/gig-detail-screen.component';
import { ProfileScreenComponent } from './component/profile-screen/profile-screen.component';
import { GigsByUserScreenComponent } from './component/gigs-by-user-screen/gigs-by-user-screen.component';

export const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardScreenComponent,
  },
  {
    path: 'gig/:id',
    component: GigDetailScreenComponent,
  },
  {
    path:'gig-by-user/:userId',
    component:GigsByUserScreenComponent
  },
  {
    path: 'profile/:id',
    component: ProfileScreenComponent,
    // children: [
    //   {
    //     path: 'gigs',
    //     component: GigsByUserScreenComponent,
    //   },
    // ],
  },
];
