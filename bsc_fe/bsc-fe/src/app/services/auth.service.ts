import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { UserRole } from '../enums/role';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  http = inject(HttpClient);
  private tokenKey = 'auth_token';
  private currentUserObject = new BehaviorSubject<User | null>(null);
  constructor() {}

  currentUser$ = this.currentUserObject.asObservable();

  setUser(user: User | null): void {
    this.currentUserObject.next(user);
    console.log(user);
  }

  getUser(): User | null {
    return this.currentUserObject.value;
  }

  getUserId(): number | null {
    const user = this.getUser();
    return user ? user.id : null;
  }
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

  handleLoggedIn(user: User, token: string): void {
    this.setUser(user);
    this.setToken(token);
  }

  login(username: string, password: string) {
    const loginData = { username, password };
    return this.http.post<LoginResponse>(
      `${environment.apiUrl}/auth/login`,
      loginData,
    );
  }
}

export interface User {
  id: number;
  name: string;
  email: string;
  role: UserRole;
}

interface LoginResponse {
  jwt: string;
  user: User;
}
