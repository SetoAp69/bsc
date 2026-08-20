import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  http = inject(HttpClient);
  private tokenKey = 'auth_token';
  constructor() { }

  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
  isLoggedIn(): boolean {
    const token = this.getToken();
    return !!token;
  }
  logout(): void {
    localStorage.removeItem(this.tokenKey);
  }

  login(username: string, password: string) {
    const loginData = { username, password };
    return this.http.post<{ token: string; user: User }>(`${environment.apiUrl}/auth/login`, loginData);
  }
}

interface User {
  id: number;
  name: string;
  email: string;
}
