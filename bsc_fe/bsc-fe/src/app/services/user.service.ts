import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  http = inject(HttpClient);
  constructor() { }

  getUserById(userId: number) {
    return this.http.get<UserResponse>(`/api/users/${userId}`);
  }
}

interface UserResponse {
  id: number;
  name: string;
  email: string;
}
