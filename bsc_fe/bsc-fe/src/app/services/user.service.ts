import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environment/env';
import { Observable } from 'rxjs';
import { User } from '../interface/user.interface';

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
