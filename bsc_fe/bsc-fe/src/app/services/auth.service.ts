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
  private localUserSubject = new BehaviorSubject<User | null>(null);

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
          this.localUserSubject.next(JSON.parse(e ?? '') as User);
        },
      });
  }
  currentUser$ = this.localUserSubject.asObservable();
  setUser(user: User | null): void {
    localStorage.setItem(this.userKey, JSON.stringify(user));
  }

  getUser(): User | null {
    try{
      const stringifyUser = localStorage.getItem(this.userKey)??'';
      const user: User = JSON.parse(stringifyUser) as User;
      return user;
    }catch(e :any){
      return null
    }
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
    localStorage.removeItem(this.userKey);
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
  userRole: UserRole;
}

interface LoginResponse {
  jwt: string;
  user: User;
}
