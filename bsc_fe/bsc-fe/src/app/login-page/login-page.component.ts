import { Component, inject } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css'
})
export class LoginPageComponent {
  fb = inject(FormBuilder);
  authService = inject(AuthService);
  loginForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });

  OnSubmit(): void {
    const { username, password } = this.loginForm.value;
    if (username && password) {
    this.authService.login(username, password).subscribe({
      next: (response) => {
        const token = response.token;
        this.authService.setToken(token);
        console.log('Login successful. Token stored in local storage.');
        console.log('User', response.user);
      },
      error: (error) => {
        console.error('Login failed:', error);
      }
    });
  }
  }
}
