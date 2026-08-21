import { Component, inject } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { usernameValidator } from '../../validators/username-validator';
import { LoadingComponent } from '../loading/loading.component';

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, LoadingComponent],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css'
})
export class LoginPageComponent {
  fb = inject(FormBuilder);
  router = inject(Router);
  authService = inject(AuthService);
  isLoading = false;
  loginForm = this.fb.group({ 
    username: ['', [usernameValidator, Validators.required, Validators.minLength(4)]],
    password: ['', [Validators.required, Validators.minLength(4)]]
  });

  OnSubmit(): void {
    const { username, password } = this.loginForm.value;
    if (username && password) {
      this.isLoading = true;
      this.authService.login(username, password).subscribe({
        next: (response) => {
          const token = response.jwt;
          this.authService.handleLoggedIn(response.user, token);
          this.router.navigate([`/dashboard`]);
        },
        error: (error) => {
          console.error('Login failed:', error);
        },
        complete: () => {
          this.isLoading = false;
        }
      });
    }
  }
}
