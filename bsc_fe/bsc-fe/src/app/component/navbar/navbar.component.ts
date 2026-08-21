import { Component, inject } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [NgbModule, RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  private authService = inject(AuthService);
  private route = inject(Router);
  user = this.authService.getUser();
  userId = this.user?.id ?? 0;
  userName = this.user?.name ?? '';

  constructor() {
    this.authService.currentUser$.subscribe((user) => {
      this.user = user;
      this.userId = user?.id ?? 0;
      this.userName = user?.name ?? '';
    });
  }

  onLogout() {
    this.authService.logout();
    this.route.navigate(['login']);
  }
}
