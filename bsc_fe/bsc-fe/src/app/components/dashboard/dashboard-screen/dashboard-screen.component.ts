import { Component, inject } from '@angular/core';
import { GigListComponent } from '../../shared/gig-list/gig-list.component';


@Component({
  selector: 'app-dashboard-screen',
  standalone: true,
  imports: [GigListComponent],
  templateUrl: './dashboard-screen.component.html',
  styleUrl: './dashboard-screen.component.css',
})
export class DashboardScreenComponent {
 
}
