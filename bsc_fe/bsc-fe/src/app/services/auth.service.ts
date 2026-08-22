import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { UserRole } from '../enums/user-role';
import { filter, fromEvent, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  http = inject(HttpClient);
  private tokenKey = 'auth_token';
  private userKey = 'user_data';
  private localUserSubject = new BehaviorSubject<User | null>(this.getUser());

  constructor() {
    fromEvent<StorageEvent>(window, 'storage')
      .pipe(
        filter(
          (event) =>
            event.storageArea === localStorage && event.key == this.userKey,
        ),
        map((event) => event.newValue),
      )
      .subscribe({
        next: (e) => {
          this.localUserSubject.next(parseUserFromJson(e??''));
        },
      });
  }
  currentUser$ = this.localUserSubject.asObservable();
  setUser(user: User | null): void {
    if (user === null) {
      localStorage.removeItem(this.userKey);
    } else {
      localStorage.setItem(this.userKey, JSON.stringify(user));
    }
    this.localUserSubject.next(user);
  }

  getUser(): User | null {
    const stringifyUser = localStorage.getItem(this.userKey) ?? '';
    return parseUserFromJson(stringifyUser);
  }

  getUserId(): number | null {
    const user = this.getUser();
    return user ? user.id : null;
  }

  getUserRole(): UserRole | null {
    const user = this.getUser();
    return user ? user.userRole : null;
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
    this.setUser(null);
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

function parseUserFromJson(stringifyUser: string): User | null {
  try {
    return JSON.parse(stringifyUser) as User;
  } catch {
    return null;
  }
}
export interface User {
  id: number;
  name: string;
  email: string;
  userRole: UserRole;
}

interface LoginResponse {
  jwt: string;
  user: User;
}
