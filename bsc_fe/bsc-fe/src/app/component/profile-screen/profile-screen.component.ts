import { Component, inject, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import {
  ActivatedRoute,
  RouterLinkActive,
  RouterLink,
  RouterOutlet,
} from '@angular/router';
import { User } from '../../interface/user.interface';
import { CommonModule } from '@angular/common';
import { GigListComponent } from '../dashboard/gig-list/gig-list.component';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-profile-screen',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    RouterOutlet,
    GigListComponent,
  ],
  templateUrl: './profile-screen.component.html',
  styleUrl: './profile-screen.component.css',
})
export class ProfileScreenComponent implements OnInit {
  ngOnInit(): void {
    this.fetchProfile();
  }
  private userService = inject(UserService);
  private authService = inject(AuthService);
  private route = inject(ActivatedRoute);
  profile: User = {
    id: 0,
    username: '',
    name: '',
    email: '',
    about: null,
    location: null,
    userRole: '',
    rating: null,
  };
  id = +(this.route.snapshot.paramMap.get('id') ?? '');
  isLoading = false;
  isFailed = false;

  fetchProfile() {
    this.isLoading = true;
    this.isFailed = false;
    this.userService.getUserProfile(this.id).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.profile = res;
      },
    });
  }
}
